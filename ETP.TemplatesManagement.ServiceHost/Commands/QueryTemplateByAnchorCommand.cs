using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Commands;

public class QueryTemplateByAnchorCommand : IRequest<Result<TemplateDto>>
{
    public Guid DeliveryOwnerId { get; set; }
    public Guid ServiceLineId { get; set; }
    public Guid MarketOfferingId { get; set; }
    
    public QueryTemplateByAnchorCommand(Guid deliveryOwnerId, Guid serviceLineId, Guid marketOfferingId)
    {
        DeliveryOwnerId = deliveryOwnerId;
        ServiceLineId = serviceLineId;
        MarketOfferingId = marketOfferingId;
    }
}