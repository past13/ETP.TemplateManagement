using MediatR;
using ETP.TemplatesManagement.SDK.Dto;
using ETP.TemplatesManagement.ServiceHost.Domain.Request;
using ETP.TemplatesManagement.ServiceHost.Domain.Response;

namespace ETP.TemplatesManagement.ServiceHost.Commands;

public class QueryTemplatesByAnchorCommand : IRequest<Result<List<TemplateDto>>>
{
    public IReadOnlyList<Guid> DeliveryOwnerIds { get; set; }
    public IReadOnlyList<Guid> ServiceLineIds { get; set; }
    public IReadOnlyList<Guid> MarketOfferingIds { get; set; }
    public IReadOnlyList<string> DeliveryOwnerNames { get; set; }
    public IReadOnlyList<string> ServiceLineNames { get; set; }
    public IReadOnlyList<string> MarketOfferingNames { get; set; }
    
    public QueryTemplatesByAnchorCommand(AnchorPointsRequest request)
    {
        DeliveryOwnerIds = request.DeliveryOwnerIds;
        ServiceLineIds = request.ServiceLineIds;
        MarketOfferingIds = request.MarketOfferingIds;
        DeliveryOwnerNames = request.DeliveryOwnerNames;
        ServiceLineNames = request.ServiceLineNames;
        MarketOfferingNames = request.MarketOfferingNames;
    }
}