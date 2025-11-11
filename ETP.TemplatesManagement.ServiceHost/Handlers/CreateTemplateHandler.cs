using AutoMapper;
using ETP.TemplatesManagement.Data.Entities;
using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Commands;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Handlers;

public class CreateTemplateHandler : IRequestHandler<CreateTemplateCommand, Result<TemplateDto>>
{
    private readonly ITemplateRepository _repository;
    private readonly IMapper _mapper;

    public CreateTemplateHandler(ITemplateRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<Result<TemplateDto>> Handle(CreateTemplateCommand command, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        
        try
        {
            var entity = new TemplateEntity
            {
                Id = Guid.NewGuid(),
                Title = command.Title,
                AnchorPoint = new AnchorPointEntity
                {
                    DeliveryOwnerId = command.AnchorPoint.DeliveryOwnerId != Guid.Empty ? command.AnchorPoint.DeliveryOwnerId : Guid.NewGuid(),
                    DeliveryOwnerName = command.AnchorPoint.DeliveryOwnerName,
                    ServiceLineId = command.AnchorPoint.ServiceLineId != Guid.Empty ? command.AnchorPoint.ServiceLineId : Guid.NewGuid(),
                    ServiceLineName = command.AnchorPoint.ServiceLineName,
                    MarketOfferingId = command.AnchorPoint.MarketOfferingId != Guid.Empty ? command.AnchorPoint.MarketOfferingId : Guid.NewGuid(),
                    MarketOfferingName = command.AnchorPoint.MarketOfferingName
                },
                Attributes = command.Attributes.Select(a => new TemplateAttributeEntity
                {
                    AttributeId = a.AttributeId != Guid.Empty ? a.AttributeId : Guid.NewGuid(),
                    AttributeName = a.AttributeName,
                    AttributeDescription = a.AttributeDescription
                }).ToList(),
            
                CreatedAt = DateTimeOffset.UtcNow
            };
            
            await _repository.CreateTemplate(entity, cancellationToken);

            var result = _mapper.Map<TemplateDto>(entity);
            
            return Result<TemplateDto>.Success(result);
        }
        catch (Exception ex)
        {
            return Result<TemplateDto>.Failure(ex.Message);
        }
    }
}