using ETP.TemplatesManagement.Data.Entities;

namespace ETP.TemplatesManagement.RA.Repositories;

public interface ITemplateRepository
{
    Task<TemplateEntity> CreateTemplate(TemplateEntity template);
    Task<TemplateEntity> UpdateTemplate(TemplateEntity template);
    Task<TemplateEntity> GetTemplateById(Guid id);
    Task<bool> DeleteTemplate(Guid id);
    Task<TemplateEntity?> GetTemplateByAnchor(Guid deliveryOwnerId, Guid serviceLineId, Guid marketOfferingId);
    Task<List<TemplateEntity>> SearchTemplates(Guid? deliveryOwnerId, Guid? serviceLineId, Guid? marketOfferingId);
}