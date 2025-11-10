using TemplateManagement.Domain.Entities;
using TemplateManagement.Domain.Request;
using TemplateManagement.Domain.Response;

namespace TemplateManagement.Repositories;

public interface ITemplateRepository
{
    Task<TemplateEntity> CreateTemplate(TemplateEntity template);
    Task<TemplateEntity> GetTemplate(Guid id, string title);
    Task<TemplateEntity?> GetTemplateByAnchor(Guid deliveryOwnerId, Guid serviceLineId, Guid marketOfferingId);
    Task<bool> UpdateTemplate(TemplateEntity template);
    Task<bool> DeleteTemplate(Guid id);
    Task<PagedResult<TemplateEntity>> SearchTemplates(TemplateSearchRequest request);
}