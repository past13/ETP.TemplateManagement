namespace ETP.TemplatesManagement.SDK.Dto;

public class AnchorPointDto
{
    public Guid DeliveryOwnerId { get; set; } 
    public string DeliveryOwnerName { get; set; }

    public Guid ServiceLineId { get; set; } 
    public string ServiceLineName { get; set; }

    public Guid MarketOfferingId { get; set; }
    public string MarketOfferingName { get; set; }
}