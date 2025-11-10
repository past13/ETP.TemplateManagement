using MediatR;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;

namespace ETP.TemplatesManagement.ServiceHost.Commands;

public class QueryTemplatesCommand : IRequest<Result<List<TemplateDto>>>
{
    public Guid? DeliveryOwnerId { get; set; }
    public Guid? ServiceLineId { get; set; }
    public Guid? MarketOfferingId  { get; set; }
    
    public QueryTemplatesCommand(Guid deliveryOwnerId, Guid serviceLineId, Guid marketOfferingId)
    {
        DeliveryOwnerId = deliveryOwnerId;
        ServiceLineId = serviceLineId;
        MarketOfferingId = marketOfferingId;
    }
}