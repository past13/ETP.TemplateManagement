using MediatR;
using TemplateManagement.Commands;
using TemplateManagement.Domain.Entities;
using TemplateManagement.Domain.Response;
using TemplateManagement.Repositories;

namespace TemplateManagement.Handlers;

public class GetTemplateHandler : IRequestHandler<GetTemplateCommand, Result<TemplateEntity>>
{
    private readonly ITemplateRepository _repository;
    public GetTemplateHandler(ITemplateRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<TemplateEntity>> Handle(GetTemplateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.GetTemplate(command.Id, command.Title);
            
            return Result<TemplateEntity>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<TemplateEntity>.Failure(ex.Message);
        }
    }
}