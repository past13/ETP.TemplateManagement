namespace ETP.TemplatesManagement.SDK.Dto;

public class TemplateDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    
    public AnchorPointDto AnchorPoint { get; set; } = null!;
    public List<TemplateAttributeDto> Attributes { get; set; } = new();
    
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedAt { get; set; }
}