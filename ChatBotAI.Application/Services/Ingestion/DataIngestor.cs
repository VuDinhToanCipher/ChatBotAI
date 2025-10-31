﻿using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.VectorData;

namespace ChatApp1.Services.Ingestion;

public class DataIngestor(
    ILogger<DataIngestor> logger,
    VectorStoreCollection<string, IngestedChunk> chunksCollection,
    VectorStoreCollection<string, IngestedDocument> documentsCollection,
    IEmbeddingGenerator<string, Embedding<float>> embeddingGenerator) 
{
    public static async Task IngestDataAsync(IServiceProvider services, IIngestionSource source)
    {
        using var scope = services.CreateScope();
        var ingestor = scope.ServiceProvider.GetRequiredService<DataIngestor>();
        await ingestor.IngestDataAsync(source);
    }

    public async Task IngestDataAsync(IIngestionSource source)
    {
        await chunksCollection.EnsureCollectionExistsAsync();
        await documentsCollection.EnsureCollectionExistsAsync();

        var sourceId = source.SourceId;
        var documentsForSource = await documentsCollection.GetAsync(doc => doc.SourceId == sourceId, top: int.MaxValue).ToListAsync();
        var deletedDocuments = await source.GetDeletedDocumentsAsync(documentsForSource);

        foreach (var deletedDocument in deletedDocuments)
        {
            logger.LogInformation("Removing ingested data for {documentId}", deletedDocument.DocumentId);
            await DeleteChunksForDocumentAsync(deletedDocument);
            await documentsCollection.DeleteAsync(deletedDocument.Key);
        }

        var modifiedDocuments = await source.GetNewOrModifiedDocumentsAsync(documentsForSource);

        foreach (var modifiedDocument in modifiedDocuments)
        {
            logger.LogInformation("Processing {documentId}", modifiedDocument.DocumentId);
            await DeleteChunksForDocumentAsync(modifiedDocument);
            await documentsCollection.UpsertAsync(modifiedDocument);

            var newRecords = await source.CreateChunksForDocumentAsync(modifiedDocument);

            // ✨✨✨ FIX: GENERATE EMBEDDINGS CHO TỪNG CHUNK ✨✨✨
            logger.LogInformation("Generating embeddings for {chunkCount} chunks from {documentId}",
                newRecords.Count(), modifiedDocument.DocumentId);

            var chunksWithEmbeddings = new List<IngestedChunk>();
            foreach (var chunk in newRecords)
            {
                try
                {
                    // Thử các cách gọi khác nhau tùy version
                    var embeddingResult = await embeddingGenerator.GenerateAsync(new[] { chunk.Text });
                    var embedding = embeddingResult.FirstOrDefault();

                    if (embedding != null && embedding.Vector.Length > 0)
                    {
                        chunk.Vector = embedding.Vector;
                        chunksWithEmbeddings.Add(chunk);
                        logger.LogDebug("Generated embedding for chunk {key} ({dimensions} dims)",
                            chunk.Key, embedding.Vector.Length);
                    }
                    else
                    {
                        logger.LogWarning("Empty embedding generated for chunk {key}", chunk.Key);
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Failed to generate embedding for chunk {key}", chunk.Key);
                    // Skip chunk nếu không generate được embedding
                }
            }

            if (chunksWithEmbeddings.Any())
            {
                await chunksCollection.UpsertAsync(chunksWithEmbeddings);
                logger.LogInformation("Successfully ingested {count} chunks with embeddings",
                    chunksWithEmbeddings.Count);
            }
            // ✨✨✨ HẾT PHẦN FIX ✨✨✨
        }

        logger.LogInformation("Ingestion is up-to-date");

        async Task DeleteChunksForDocumentAsync(IngestedDocument document)
        {
            var documentId = document.DocumentId;
            var chunksToDelete = await chunksCollection.GetAsync(record => record.DocumentId == documentId, int.MaxValue).ToListAsync();
            if (chunksToDelete.Any())
            {
                await chunksCollection.DeleteAsync(chunksToDelete.Select(r => r.Key));
            }
        }
    }
}