namespace ETP.TemplatesManagement.ServiceHost.Domain.Request;

public record TemplateSearchRequest(
    int Page = 1,
    int PageSize = 20,
    string? SortBy = "Title",
    string? SortDirection = "asc",
    Guid? DeliveryOwnerId = null,
    Guid? ServiceLineId = null,
    Guid? MarketOfferingId = null
);