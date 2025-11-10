using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TemplateManagement.Domain.Entities;

public class TemplateEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; }
    
    [BsonElement("Name")]
    public string Title { get; set; }
    
    public AnchorPoint AnchorPoint { get; set; } = null!;
    public List<TemplateAttribute> Attributes { get; set; } = new();
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
}