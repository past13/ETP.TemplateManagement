using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Domain.Request;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Commands;

public class UpdateTemplateCommand : IRequest<Result<TemplateDto>>
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public List<TemplateAttributeRequest> Attributes { get; set; }
    public UpdateTemplateCommand(Guid id, string title, List<TemplateAttributeRequest> attributes)
    {
        Id = id;
        Title = title;
        Attributes =  attributes;
    }
}