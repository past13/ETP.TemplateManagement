using MediatR;
using TemplateManagement.Commands;
using TemplateManagement.Domain.Entities;
using TemplateManagement.Domain.Response;
using TemplateManagement.Repositories;

namespace TemplateManagement.Handlers;

public class GetTemplateByAnchorHandler : IRequestHandler<GetTemplateByAnchorCommand, Result<TemplateEntity>>
{
    private readonly ITemplateRepository _repository;
    public GetTemplateByAnchorHandler(ITemplateRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Result<TemplateEntity>> Handle(GetTemplateByAnchorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _repository.GetTemplateByAnchor(command.DeliveryOwnerId, command.ServiceLineId, command.MarketOfferingId);

            return Result<TemplateEntity>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<TemplateEntity>.Failure(ex.Message);
        }
    }
}