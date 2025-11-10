namespace ETP.TemplatesManagement.ServiceHost.Domain.Request;

public class UpdateTemplateRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<TemplateAttributeRequest> Attributes { get; set; } 
}