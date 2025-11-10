namespace ETP.TemplatesManagement.ServiceHost.Domain.Request;

public class CreateTemplateRequest
{
    public string Title { get; set; }
    public AnchorPointRequest AnchorPoint { get; set; }
    public List<TemplateAttributeRequest> Attributes { get; set; } = new();
}