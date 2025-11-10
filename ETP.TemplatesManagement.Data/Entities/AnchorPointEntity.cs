using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ETP.TemplatesManagement.Data.Entities;

public class AnchorPointEntity
{
    [BsonRepresentation(BsonType.String)]
    public Guid DeliveryOwnerId { get; set; } 
    public string DeliveryOwnerName { get; set; }

    [BsonRepresentation(BsonType.String)]
    public Guid ServiceLineId { get; set; } 
    public string ServiceLineName { get; set; }
    
    [BsonRepresentation(BsonType.String)]
    public Guid MarketOfferingId { get; set; }
    public string MarketOfferingName { get; set; }
    
}