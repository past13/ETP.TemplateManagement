using MediatR;
using TemplateManagement.Commands;
using TemplateManagement.Domain.Entities;
using TemplateManagement.Domain.Response;
using TemplateManagement.Repositories;

namespace TemplateManagement.Handlers;

public class UpdateTemplateHandler : IRequestHandler<UpdateTemplateCommand, Result<TemplateEntity>>
{
    private readonly ITemplateRepository _repository;
    public UpdateTemplateHandler(ITemplateRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<TemplateEntity>> Handle(UpdateTemplateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var template = new TemplateEntity
            {
                Id = command.Id,
                Title = command.Title,
                Attributes = command.Attributes,
            };
            
            await _repository.UpdateTemplate(template);
            
            return Result<TemplateEntity>.Success(template);
        }
        catch (Exception ex)
        {
            return Result<TemplateEntity>.Failure(ex.Message);
        }
    }
}