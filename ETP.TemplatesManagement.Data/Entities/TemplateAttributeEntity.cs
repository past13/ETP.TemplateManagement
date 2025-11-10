using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ETP.TemplatesManagement.Data.Entities;

public class TemplateAttributeEntity
{
    [BsonRepresentation(BsonType.String)]
    public Guid AttributeId { get; set; }
    public string AttributeName { get; set; }
    public string AttributeDescription { get; set; }
}