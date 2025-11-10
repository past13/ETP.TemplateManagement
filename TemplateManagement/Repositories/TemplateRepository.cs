using MongoDB.Driver;
using MongoDB.Driver.Linq;
using TemplateManagement.Domain.Entities;
using TemplateManagement.Domain.Request;
using TemplateManagement.Domain.Response;
using TemplateManagement.Helpers;

namespace TemplateManagement.Repositories;

public class TemplateRepository : ITemplateRepository
{
    private readonly IMongoCollection<TemplateEntity> _collection;

    public TemplateRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<TemplateEntity>("Templates");
    }

    public async Task<TemplateEntity> CreateTemplate(TemplateEntity template)
    {
        var duplicateFilter = Builders<TemplateEntity>.Filter.Eq(t => t.Title, template.Title) &
                              Builders<TemplateEntity>.Filter.Ne(t => t.Id, template.Id);

        var exists = await _collection.Find(duplicateFilter).AnyAsync();
        if (exists)
        {
            throw new InvalidOperationException($"A template with the title '{template.Title}' already exists.");
        }
        
        template.CreatedAt = DateTimeOffset.UtcNow;
        template.UpdatedAt = DateTimeOffset.UtcNow;

        await _collection.InsertOneAsync(template);
        return template;
    }

    public async Task<TemplateEntity> GetTemplate(Guid id, string title)
    {
        var filter = Builders<TemplateEntity>.Filter.Eq(t => t.Id, id) & 
                     Builders<TemplateEntity>.Filter.Eq(t => t.Title, title);
        
        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task<TemplateEntity?> GetTemplateByAnchor(Guid deliveryOwnerId, Guid serviceLineId, Guid marketOfferingId)
    {
        var filter = Builders<TemplateEntity>.Filter.Eq(t => t.AnchorPoint.DeliveryOwnerId, deliveryOwnerId) &
                     Builders<TemplateEntity>.Filter.Eq(t => t.AnchorPoint.ServiceLineId, serviceLineId) &
                     Builders<TemplateEntity>.Filter.Eq(t => t.AnchorPoint.MarketOfferingId, marketOfferingId);

        return await _collection.Find(filter).FirstOrDefaultAsync();
    }
    
    public async Task<bool> UpdateTemplate(TemplateEntity template)
    {
        var duplicateFilter = Builders<TemplateEntity>.Filter.Eq(t => t.Title, template.Title) &
                              Builders<TemplateEntity>.Filter.Eq(t => t.Id, template.Id);

        var exists = await _collection.Find(duplicateFilter).AnyAsync();
        if (exists)
        {
            throw new InvalidOperationException($"A template with the title '{template.Title}' already exists.");
        }
        
        var filter = Builders<TemplateEntity>.Filter.Eq(t => t.Id, template.Id);
        var update = Builders<TemplateEntity>.Update
            .Set(t => t.Title, template.Title)
            .Set(t => t.Attributes, template.Attributes)
            .Set(t => t.UpdatedAt, DateTimeOffset.UtcNow);

        var result = await _collection.UpdateOneAsync(filter, update);
        
        return result.ModifiedCount > 0;
    }
    
    public async Task<bool> DeleteTemplate(Guid id)
    {
        var filter = Builders<TemplateEntity>.Filter.Eq(t => t.Id, id);
        var result = await _collection.DeleteOneAsync(filter);
        return result.DeletedCount > 0;
    }
    
    public async Task<PagedResult<TemplateEntity>> SearchTemplates(TemplateSearchRequest request)
    {
        var query = _collection.AsQueryable();
        
        if (request.DeliveryOwnerId.HasValue)
        {
            query = query.Where(t => t.AnchorPoint.DeliveryOwnerId == request.DeliveryOwnerId.Value);
        }

        if (request.ServiceLineId.HasValue)
        {
            query = query.Where(t => t.AnchorPoint.ServiceLineId == request.ServiceLineId.Value);
        }

        if (request.MarketOfferingId.HasValue)
        {
            query = query.Where(t => t.AnchorPoint.MarketOfferingId == request.MarketOfferingId.Value);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .ApplySorting(request.SortBy, request.SortDirection)
            .ApplyPaging(request.Page, request.PageSize)
            .ToListAsync();

        return new PagedResult<TemplateEntity>
        {
            Items = items,
            Page = request.Page,
            PageSize = request.PageSize,
            TotalCount = totalCount
        };
    }
}