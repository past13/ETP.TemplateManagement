namespace ETP.TemplatesManagement.ServiceHost.Domain.Request;

public class SearchAnchorPointRequest
{
    public Guid? DeliveryOwnerId { get; set; }
    public Guid? ServiceLineId { get; set; }
    public Guid? MarketOfferingId  { get; set; }
}