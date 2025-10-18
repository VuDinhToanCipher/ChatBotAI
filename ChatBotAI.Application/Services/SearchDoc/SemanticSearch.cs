using Microsoft.Extensions.VectorData;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApp1.Services
{
    public class SemanticSearch
    {
        private readonly VectorStoreCollection<string, IngestedChunk> _vectorCollection;

        public SemanticSearch(VectorStoreCollection<string, IngestedChunk> vectorCollection)
        {
            _vectorCollection = vectorCollection ?? throw new ArgumentNullException(nameof(vectorCollection));
        }

        public async Task<IReadOnlyList<IngestedChunk>> SearchAsync(string text, string? documentIdFilter, int maxResults)
        {
            try
            {
                // Kiểm tra nếu text là câu chào hỏi hoặc quá ngắn
                if (string.IsNullOrWhiteSpace(text) || IsCasualGreeting(text))
                {
                    Console.WriteLine($"Bỏ qua tìm kiếm cho câu: '{text}' (chào hỏi hoặc không hợp lệ)");
                    return new List<IngestedChunk>();
                }

                var searchOptions = new VectorSearchOptions<IngestedChunk>
                {
                    Filter = documentIdFilter is { Length: > 0 } ? record => record.DocumentId == documentIdFilter : null,
                };

                var nearest = _vectorCollection.SearchAsync(text, maxResults, searchOptions);
                var results = await nearest.Select(result => result.Record).ToListAsync();

                Console.WriteLine($"Tìm kiếm cho '{text}' trả về {results.Count} kết quả.");
                return results;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi tìm kiếm: {ex.Message}");
                return new List<IngestedChunk>();
            }
        }
        private bool IsCasualGreeting(string text)
        {
            var greetings = new[] { "xin chào", "hi", "hello", "chào bạn", "chào", "hế lô" };
            text = text.Trim().ToLower();
            return greetings.Any(g => text.Contains(g)) && text.Length < 20; // Giới hạn độ dài để tránh nhầm với câu hỏi dài
        }
    }
}