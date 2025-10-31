using Microsoft.Extensions.VectorData;

namespace ChatApp1.Services;

public class IngestedChunk
{
    private const int VectorDimensions = 768;
    private const string VectorDistanceFunction = DistanceFunction.CosineDistance;

    [VectorStoreKey]
    public required string Key { get; set; }

    [VectorStoreData(IsIndexed = true)]
    public required string DocumentId { get; set; }

    [VectorStoreData]
    public int PageNumber { get; set; }

    [VectorStoreData]
    public required string Text { get; set; }

    // FIX: Vector phải trả về Text để VectorStore tự động embedding
    // Nhưng đảm bảo rằng EmbeddingGenerator được đăng ký đúng
    [VectorStoreVector(VectorDimensions, DistanceFunction = VectorDistanceFunction)]
    public ReadOnlyMemory<float>? Vector { get; set; }
}

// ===== CÁCH FIX CHÍNH: Sửa DataIngestor.cs =====
// Trong file DataIngestor.cs, thêm IEmbeddingGenerator để generate embedding thủ công

/*
using Microsoft.Extensions.AI;
using Microsoft.Extensions.VectorData;

public class DataIngestor(
    ILogger<DataIngestor> logger,
    VectorStoreCollection<string, IngestedChunk> chunksCollection,
    VectorStoreCollection<string, IngestedDocument> documentsCollection,
    IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator) // ← THÊM DÒNG NÀY
{
    // ... existing code ...
    
    public async Task IngestDataAsync(IIngestionSource source)
    {
        // ... existing code cho documents ...
        
        foreach (var modifiedDocument in modifiedDocuments)
        {
            logger.LogInformation("Processing {documentId}", modifiedDocument.DocumentId);
            await DeleteChunksForDocumentAsync(modifiedDocument);
            await documentsCollection.UpsertAsync(modifiedDocument);
            
            var newRecords = await source.CreateChunksForDocumentAsync(modifiedDocument);
            
            // ✨ FIX: Generate embeddings cho từng chunk
            foreach (var chunk in newRecords)
            {
                var embedding = await embeddingGenerator.GenerateEmbeddingAsync(chunk.Text);
                chunk.Vector = embedding.Vector;
            }
            
            await chunksCollection.UpsertAsync(newRecords);
        }
        
        logger.LogInformation("Ingestion is up-to-date");
    }
}
*/