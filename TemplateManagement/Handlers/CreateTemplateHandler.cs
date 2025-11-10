using MediatR;
using TemplateManagement.Commands;
using TemplateManagement.Domain.Entities;
using TemplateManagement.Domain.Response;
using TemplateManagement.Repositories;

namespace TemplateManagement.Handlers;

public class CreateTemplateHandler : IRequestHandler<CreateTemplateCommand, Result<TemplateEntity>>
{
    private readonly ITemplateRepository _repository;
    public CreateTemplateHandler(ITemplateRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<TemplateEntity>> Handle(CreateTemplateCommand command, CancellationToken cancellationToken)
    {
        var template = new TemplateEntity
        {
            Id = Guid.NewGuid(),
            Title = command.Title,
            Attributes = command.Attributes,
            AnchorPoint = command.AnchorPoint,
        };

        try
        {
            await _repository.CreateTemplate(template);
            
            return Result<TemplateEntity>.Success(template);
        }
        catch (Exception ex)
        {
            return Result<TemplateEntity>.Failure(ex.Message);
        }
    }
}