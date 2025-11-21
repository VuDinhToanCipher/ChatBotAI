using Microsoft.Extensions.VectorData;

namespace ChatApp1.Services
{
    // wrapper result with score
    public class IngestedChunkWithScore
    {
        public IngestedChunk Chunk { get; set; } = default!;
        public double Score { get; set; }
    }

    public class SemanticSearch
    {
        private readonly VectorStoreCollection<string, IngestedChunk> _vectorCollection;

        // threshold & dedupe config
        private const double ScoreThreshold = 0.0; // keep >= 0 means keep all; we will still sort by score
        private const int DefaultMaxResults = 5;

        public SemanticSearch(VectorStoreCollection<string, IngestedChunk> vectorCollection)
        {
            _vectorCollection = vectorCollection ?? throw new ArgumentNullException(nameof(vectorCollection));
        }

        // New signature: return IngestedChunk + score
        public async Task<IReadOnlyList<IngestedChunkWithScore>> SearchAsync(string text, string? documentIdFilter, int maxResults)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(text) || IsCasualGreeting(text))
                {
                    Console.WriteLine($"Bỏ qua tìm kiếm cho câu: '{text}' (chào hỏi hoặc không hợp lệ)");
                    return new List<IngestedChunkWithScore>();
                }

                var searchOptions = new VectorSearchOptions<IngestedChunk>
                {
                    Filter = documentIdFilter is { Length: > 0 } ? record => record.DocumentId == documentIdFilter : null,
                };

                // Query vector store. The result items typically include Record + Score (depends on SDK)
                var nearest = _vectorCollection.SearchAsync(text, Math.Max(1, maxResults), searchOptions);

                // Map to objects containing record + score
                var found = await nearest
                    .Select(r => new IngestedChunkWithScore
                    {
                        Chunk = r.Record,
                        Score = Convert.ToDouble(r.Score)
                    })
                    .ToListAsync();

                // If SDK doesn't provide Score for some reason, fallback to 1.0
                for (int i = 0; i < found.Count; i++)
                {
                    if (double.IsNaN(found[i].Score) || found[i].Score == 0)
                    {
                        found[i].Score = 1.0;
                    }
                }

                // Filter by score threshold (if you want stricter, raise this)
                var filtered = found
                    .Where(f => f.Score >= ScoreThreshold)
                    .OrderByDescending(f => f.Score)
                    .Take(maxResults)
                    .ToList();

                Console.WriteLine($"Tìm kiếm cho '{text}' trả về {filtered.Count} kết quả (top {maxResults}).");

                // Optional dedupe by text similarity (simple containment based)
                var dedup = new List<IngestedChunkWithScore>();
                var seen = new List<string>();
                foreach (var item in filtered)
                {
                    var key = (item.Chunk.Text ?? string.Empty).Trim().ToLower();
                    if (string.IsNullOrEmpty(key)) continue;

                    // skip if very similar to already seen
                    if (seen.Any(s => s.Contains(key) || key.Contains(s)))
                        continue;

                    seen.Add(key.Length > 200 ? key[..200] : key);
                    dedup.Add(item);
                }

                return dedup;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tìm kiếm: {ex.Message}");
                return new List<IngestedChunkWithScore>();
            }
        }

        private bool IsCasualGreeting(string text)
        {
            var greetings = new[] { "xin chào", "hi", "hello", "chào bạn", "chào", "hế lô" };
            text = text.Trim().ToLower();
            return greetings.Any(g => text.Contains(g)) && text.Length < 20;
        }
    }
}
