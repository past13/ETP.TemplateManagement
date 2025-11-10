using MediatR;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;

namespace ETP.TemplatesManagement.ServiceHost.Commands;

public class QueryTemplateCommand : IRequest<Result<TemplateDto>>
{
    public Guid Id { get; set; }
    
    public QueryTemplateCommand(Guid id)
    {
        Id = id;
    }
}