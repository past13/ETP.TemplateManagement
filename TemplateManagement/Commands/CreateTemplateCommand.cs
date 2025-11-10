using MediatR;
using TemplateManagement.Domain.Entities;
using TemplateManagement.Domain.Response;

namespace TemplateManagement.Commands;

public class CreateTemplateCommand : IRequest<Result<TemplateEntity>>
{
    public string Title { get; set; }
    public AnchorPoint AnchorPoint { get; set; }
    public List<TemplateAttribute> Attributes { get; set; }
    
    public CreateTemplateCommand(string title, AnchorPoint anchorPoint, List<TemplateAttribute> attributes)
    {
        Title = title;
        AnchorPoint = anchorPoint;
        Attributes =  attributes;
    }
}