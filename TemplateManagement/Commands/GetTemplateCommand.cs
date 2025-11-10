using MediatR;
using TemplateManagement.Domain.Entities;
using TemplateManagement.Domain.Response;

namespace TemplateManagement.Commands;

public class GetTemplateCommand : IRequest<Result<TemplateEntity>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<TemplateAttribute> Attributes { get; set; }
    
    public GetTemplateCommand(Guid id, string title)
    {
        Id = id;
        Title = title;
    }
}