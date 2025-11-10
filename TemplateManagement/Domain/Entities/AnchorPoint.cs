namespace TemplateManagement.Domain.Entities;

public class AnchorPoint
{
    public Guid DeliveryOwnerId { get; set; }
    public Guid ServiceLineId { get; set; }
    public Guid MarketOfferingId { get; set; }
}