namespace ETP.TemplatesManagement.ServiceHost.Domain.Request;

public class TemplateAttributeRequest
{
    public Guid AttributeId { get; set; }
    public string AttributeName { get; set; }
    public string AttributeDescription { get; set; }
}