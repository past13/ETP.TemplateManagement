using MediatR;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Domain.Request;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;

namespace ETP.TemplatesManagement.ServiceHost.Commands;

public class CreateTemplateCommand : IRequest<Result<TemplateDto>>
{
    public string Title { get; set; }
    public AnchorPointRequest AnchorPoint { get; set; }
    public List<TemplateAttributeRequest> Attributes { get; set; }
    
    public CreateTemplateCommand(string title, AnchorPointRequest anchorPoint, List<TemplateAttributeRequest> attributes)
    {
        Title = title;
        AnchorPoint = anchorPoint;
        Attributes =  attributes;
    }
}