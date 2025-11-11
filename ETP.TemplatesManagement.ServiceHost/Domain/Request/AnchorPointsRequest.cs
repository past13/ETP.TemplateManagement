namespace ETP.TemplatesManagement.ServiceHost.Domain.Request;

public class AnchorPointsRequest
{
    public IReadOnlyList<Guid> DeliveryOwnerIds { get; set;  }
    public IReadOnlyList<Guid> ServiceLineIds { get; set;  }
    public IReadOnlyList<Guid> MarketOfferingIds { get; set;  }
    public IReadOnlyList<string> DeliveryOwnerNames { get; set;  }
    public IReadOnlyList<string> ServiceLineNames { get; set;  }
    public IReadOnlyList<string> MarketOfferingNames { get; set;  }
}