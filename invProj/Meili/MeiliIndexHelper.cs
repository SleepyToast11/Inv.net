using Meilisearch;
using Index = Meilisearch.Index;

namespace invProj.Infrastructure.Persistence.Entities.Persistence.Entities.Meili;

public static class MeiliIndexHelper
{
    /// <summary>
    ///     Gets an existing Meilisearch index or creates it with the given primary key.
    /// </summary>
    public static async Task<Index> GetOrCreateIndexAsync(
        MeilisearchClient client,
        string indexUid,
        string primaryKey)
    {
        try
        {
            return await client.GetIndexAsync(indexUid);
        }
        catch (MeilisearchApiError e) when (e.Code == "index_not_found")
        {
            await client.CreateIndexAsync(indexUid, primaryKey);
            return await client.GetIndexAsync(indexUid);
        }
    }
}