using AutoMapper;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Commands;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Handlers;

public class QueryTemplateHandler : IRequestHandler<QueryTemplateCommand, Result<TemplateDto>>
{
    private readonly ITemplateRepository _repository;
    private readonly IMapper _mapper;
    
    public QueryTemplateHandler(ITemplateRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<TemplateDto>> Handle(QueryTemplateCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            var entity = await _repository.GetTemplateById(command.Id, cancellationToken);
            
            return Result<TemplateDto>.Success(_mapper.Map<TemplateDto>(entity));
        }
        catch (Exception ex)
        {
            return Result<TemplateDto>.Failure(ex.Message);
        }
    }
}