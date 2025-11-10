using ETP.TemplatesManagement.Data.Entities;
using ETP.TemplatesManagement.SDK.Dto;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace ETP.TemplatesManagement.RA.Repositories;

public class TemplateRepository : ITemplateRepository
{
    private readonly IMongoCollection<TemplateEntity> _collection;

    public TemplateRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<TemplateEntity>("Templates");
    }

    public async Task<TemplateEntity> CreateTemplate(TemplateEntity template)
    {
        if (await _collection.Find(t => t.Title == template.Title).AnyAsync())
        {
            throw new InvalidOperationException($"Template with title '{template.Title}' already exists.");
        }

        await _collection.InsertOneAsync(template);
        return template;
    }

    public async Task<TemplateEntity> GetTemplateById(Guid id)
    {
        var filter = Builders<TemplateEntity>.Filter.Eq(t => t.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task<TemplateEntity?> GetTemplateByAnchor(Guid deliveryOwnerId, Guid serviceLineId, Guid marketOfferingId)
    {
        var filter = Builders<TemplateEntity>.Filter.Eq(t => t.AnchorPoint.DeliveryOwnerId, deliveryOwnerId) &
                     Builders<TemplateEntity>.Filter.Eq(t => t.AnchorPoint.ServiceLineId, serviceLineId) &
                     Builders<TemplateEntity>.Filter.Eq(t => t.AnchorPoint.MarketOfferingId, marketOfferingId);

        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task<TemplateEntity> UpdateTemplate(TemplateEntity entity)
    {
        var entityExists = Builders<TemplateEntity>.Filter.Ne(t => t.Id, entity.Id);

        var exist = await _collection.Find(entityExists).FirstOrDefaultAsync();
        if (exist is null)
        {
            throw new InvalidOperationException($"Template with Id '{entity.Id}' not found.");
        }
       
        var filterUpdate = Builders<TemplateEntity>.Filter.Eq(t => t.Id, entity.Id);

        var update = Builders<TemplateEntity>.Update
            .Set(t => t.Title, entity.Title)
            .Set(t => t.Attributes, entity.Attributes)
            .Set(t => t.UpdatedAt, DateTimeOffset.UtcNow);

        var updated = await _collection.FindOneAndUpdateAsync(
            filterUpdate,
            update,
            new FindOneAndUpdateOptions<TemplateEntity>
            {
                ReturnDocument = ReturnDocument.After
            });

        return updated;
    }
    
    public async Task<bool> DeleteTemplate(Guid id)
    {
        var filter = Builders<TemplateEntity>.Filter.Eq(t => t.Id, id);
        var result = await _collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
    
    public async Task<List<TemplateEntity>> SearchTemplates(Guid? deliveryOwnerId, Guid? serviceLineId, Guid? marketOfferingId)
    {
        var query = _collection.AsQueryable();
        
        if (deliveryOwnerId.HasValue)
        {
            query = query.Where(t => t.AnchorPoint.DeliveryOwnerId == deliveryOwnerId.Value);
        }
    
        if (serviceLineId.HasValue)
        {
            query = query.Where(t => t.AnchorPoint.ServiceLineId == serviceLineId.Value);
        }
    
        if (marketOfferingId.HasValue)
        {
            query = query.Where(t => t.AnchorPoint.MarketOfferingId == marketOfferingId.Value);
        }
        
        return await query.ToListAsync();
    }
}