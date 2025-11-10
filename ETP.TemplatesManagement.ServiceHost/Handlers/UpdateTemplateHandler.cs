using AutoMapper;
using ETP.TemplatesManagement.Data.Entities;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Commands;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Handlers;

public class UpdateTemplateHandler : IRequestHandler<UpdateTemplateCommand, Result<TemplateDto>>
{
    private readonly ITemplateRepository _repository;
    private readonly IMapper _mapper;
    
    public UpdateTemplateHandler(ITemplateRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<TemplateDto>> Handle(UpdateTemplateCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var templateEntity = new TemplateEntity
            {
                Id = command.Id,
                Title = command.Title,
                Attributes = command.Attributes.Select(a => new TemplateAttributeEntity
                {
                    AttributeId = a.AttributeId,
                    AttributeName = a.AttributeName,
                    AttributeDescription = a.AttributeDescription
                }).ToList()
            };
            
            var entity = await _repository.UpdateTemplate(templateEntity);

            var result = _mapper.Map<TemplateDto>(entity);
            
            return Result<TemplateDto>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<TemplateDto>.Failure(ex.Message);
        }
    }
}