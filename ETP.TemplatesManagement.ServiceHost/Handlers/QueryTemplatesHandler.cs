using AutoMapper;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Commands;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Handlers;

public class QueryTemplatesHandler : IRequestHandler<QueryTemplatesCommand, Result<List<TemplateDto>>>
{
    private readonly ITemplateRepository _repository;
    private readonly IMapper _mapper;
    
    public QueryTemplatesHandler(ITemplateRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<List<TemplateDto>>> Handle(QueryTemplatesCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var entity = await _repository.SearchTemplates(command.DeliveryOwnerId, command.ServiceLineId, command.MarketOfferingId);
            
            var result =  _mapper.Map<List<TemplateDto>>(entity);
            
            // var totalCount = await query.CountAsync();
            //
            // var items = await query
            //     .ApplySorting(request.SortBy, request.SortDirection)
            //     .ApplyPaging(request.Page, request.PageSize)
            //     .ToListAsync();
            //
            // return new PagedResult<TemplateDto>
            // {
            //     Items = items,
            //     Page = request.Page,
            //     PageSize = request.PageSize,
            //     TotalCount = totalCount
            // };
            
            // var totalCount = result.Count;
            //
            // var items = totalCount
            //     .ApplySorting(request.SortBy, request.SortDirection)
            //     .ApplyPaging(request.Page, request.PageSize)
            //     .ToListAsync();
            
            return Result<List<TemplateDto>>.Success(null);
        }
        catch (Exception ex)
        {
            return Result<List<TemplateDto>>.Failure(ex.Message);
        }
    }
}