using ETP.TemplatesManagement.Data.Entities;

namespace ETP.TemplatesManagement.RA.Repositories;

public interface ITemplateRepository
{
    Task<TemplateEntity> CreateTemplate(TemplateEntity template, CancellationToken cancellationToken);
    Task<TemplateEntity> UpdateTemplate(TemplateEntity template, CancellationToken cancellationToken);
    Task<TemplateEntity> GetTemplateById(Guid id, CancellationToken cancellationToken);
    Task<bool> DeleteTemplate(Guid id, CancellationToken cancellationToken);

    Task<List<TemplateEntity>> GetTemplatesByAnchorPointAsync(
        IReadOnlyList<Guid> deliveryOwnerIds,
        IReadOnlyList<Guid> marketOfferingIds,
        IReadOnlyList<Guid> serviceLineIds,
        IReadOnlyList<string> deliveryOwnerNames,
        IReadOnlyList<string> marketOfferingNames,
        IReadOnlyList<string> serviceLineNames,
        CancellationToken cancellationToken);
    
    Task<List<TemplateEntity>> SearchTemplates(string? searchTerm, int? count, int? page, string? sortColumn, string? ortOrder, CancellationToken cancellationToken);
}