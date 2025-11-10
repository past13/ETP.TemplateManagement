using MediatR;
using TemplateManagement.Domain.Entities;
using TemplateManagement.Domain.Response;

namespace TemplateManagement.Commands;

public class UpdateTemplateCommand : IRequest<Result<TemplateEntity>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<TemplateAttribute> Attributes { get; set; }
    public UpdateTemplateCommand(string title, List<TemplateAttribute> attributes)
    {
        Title = title;
        Attributes =  attributes;
    }
}