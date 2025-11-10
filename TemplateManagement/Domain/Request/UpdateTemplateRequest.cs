namespace TemplateManagement.Domain.Request;

public class UpdateTemplateRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<int> Attributes { get; set; } 
}