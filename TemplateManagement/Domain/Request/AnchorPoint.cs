namespace TemplateManagement.Domain.Request;

public class AnchorPoint
{
    public Guid DeliveryOwneId { get; set; }
    public Guid ServiceLineId { get; set; }
    public Guid MarketOfferingId  { get; set; }
}