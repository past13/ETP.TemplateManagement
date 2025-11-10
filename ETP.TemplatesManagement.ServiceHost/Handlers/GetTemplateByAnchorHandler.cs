using AutoMapper;
using ETP.TemplatesManagement.Data.Entities;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Commands;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Handlers;

public class GetTemplateByAnchorHandler : IRequestHandler<QueryTemplateByAnchorCommand, Result<TemplateDto>>
{
    private readonly ITemplateRepository _repository;
    private readonly IMapper _mapper;
    
    public GetTemplateByAnchorHandler(ITemplateRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<TemplateDto>> Handle(QueryTemplateByAnchorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            // var entity =  _mapper.Map<TemplateEntity>(dto);
            //
            // var result = await _repository.GetTemplateByAnchor(command.DeliveryOwnerId, command.ServiceLineId, command.MarketOfferingId);
            //
            //
            //
            // return Result<TemplateDto>.Success(result);
            
            return Result<TemplateDto>.Success(null);
        }
        catch (Exception ex)
        {
            return Result<TemplateDto>.Failure(ex.Message);
        }
    }
}