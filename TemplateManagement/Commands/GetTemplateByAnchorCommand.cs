using MediatR;
using TemplateManagement.Domain.Entities;
using TemplateManagement.Domain.Response;

namespace TemplateManagement.Commands;

public class GetTemplateByAnchorCommand : IRequest<Result<TemplateEntity>>
{
    public Guid DeliveryOwnerId { get; set; }
    public Guid ServiceLineId { get; set; }
    public Guid MarketOfferingId { get; set; }
    
    public GetTemplateByAnchorCommand(Guid deliveryOwnerId, Guid serviceLineId, Guid marketOfferingId)
    {
        DeliveryOwnerId = deliveryOwnerId;
        ServiceLineId = serviceLineId;
        MarketOfferingId = marketOfferingId;
    }
}