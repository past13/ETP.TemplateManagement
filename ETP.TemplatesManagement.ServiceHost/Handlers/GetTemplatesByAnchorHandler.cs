using AutoMapper;
using MediatR;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Commands;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;

namespace ETP.TemplatesManagement.ServiceHost.Handlers;

public class GetTemplatesByAnchorHandler : IRequestHandler<QueryTemplatesByAnchorCommand, Result<List<TemplateDto>>>
{
    private readonly ITemplateRepository _repository;
    private readonly IMapper _mapper;
    
    public GetTemplatesByAnchorHandler(ITemplateRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<List<TemplateDto>>> Handle(QueryTemplatesByAnchorCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            var entities = await _repository.GetTemplatesByAnchorPointAsync(
                command.DeliveryOwnerIds, 
                command.MarketOfferingIds, 
                command.ServiceLineIds,
                command.DeliveryOwnerNames,
                command.MarketOfferingNames,
                command.ServiceLineNames,
                cancellationToken);

            var result = _mapper.Map<List<TemplateDto>>(entities);
            
            return Result<List<TemplateDto>>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<List<TemplateDto>>.Failure(ex.Message);
        }
    }
}