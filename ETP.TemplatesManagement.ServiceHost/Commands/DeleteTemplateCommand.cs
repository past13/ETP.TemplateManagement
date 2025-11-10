using ETP.TemplatesManagement.ServiceHost.Domain.Response;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Commands;

public class DeleteTemplateCommand : IRequest<Result<bool>>
{
    public Guid Id { get; set; }
    
    public DeleteTemplateCommand(Guid id)
    {
        Id = id;
    }
}