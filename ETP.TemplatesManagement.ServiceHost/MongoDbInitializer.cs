using ETP.TemplatesManagement.Data.Entities;
using MongoDB.Driver;

namespace ETP.TemplatesManagement.ServiceHost;

public static class MongoDbInitializer
{
    public static void EnsureIndexes(IMongoDatabase database)
    {
        var collection = database.GetCollection<TemplateEntity>("Templates");

        // Unique Title across all templates
        var titleIndex = Builders<TemplateEntity>.IndexKeys.Ascending(t => t.Title);
        var titleIndexOptions = new CreateIndexOptions { Unique = true };
        collection.Indexes.CreateOne(new CreateIndexModel<TemplateEntity>(titleIndex, titleIndexOptions));

        var anchorIndex = Builders<TemplateEntity>.IndexKeys
            .Ascending(t => t.AnchorPoint.DeliveryOwnerId)
            .Ascending(t => t.AnchorPoint.ServiceLineId)
            .Ascending(t => t.AnchorPoint.MarketOfferingId);
        collection.Indexes.CreateOne(new CreateIndexModel<TemplateEntity>(anchorIndex));
    }
}