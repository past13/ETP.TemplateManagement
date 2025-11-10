using MongoDB.Driver;
using TemplateManagement.Domain.Entities;

namespace TemplateManagement;

public static class MongoDbInitializer
{
    public static void EnsureIndexes(IMongoDatabase database)
    {
        var collection = database.GetCollection<TemplateEntity>("Templates");

        var indexKeys = Builders<TemplateEntity>.IndexKeys
            .Ascending(t => t.AnchorPoint.DeliveryOwnerId)
            .Ascending(t => t.AnchorPoint.ServiceLineId)
            .Ascending(t => t.AnchorPoint.MarketOfferingId)
            .Ascending(t => t.Title);

        var indexOptions = new CreateIndexOptions { Unique = true };

        collection.Indexes.CreateOne(new CreateIndexModel<TemplateEntity>(indexKeys, indexOptions));
    }
}