using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.ServiceHost.Commands;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Handlers;

public class DeleteTemplateHandler : IRequestHandler<DeleteTemplateCommand, Result<bool>>
{
    private readonly ITemplateRepository _repository;
    
    public DeleteTemplateHandler(ITemplateRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<bool>> Handle(DeleteTemplateCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            await _repository.DeleteTemplate(command.Id, cancellationToken);
            return Result<bool>.Success(true);
        }
        catch (Exception ex)
        {
            return Result<bool>.Failure(ex.Message);
        }
    }
}