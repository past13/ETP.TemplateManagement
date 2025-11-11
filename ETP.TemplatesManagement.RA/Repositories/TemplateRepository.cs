using System.Linq.Expressions;
using System.Text.RegularExpressions;
using MongoDB.Bson;
using MongoDB.Driver;
using ETP.TemplatesManagement.Data.Entities;

namespace ETP.TemplatesManagement.RA.Repositories;

public class TemplateRepository : ITemplateRepository
{
    private readonly IMongoCollection<TemplateEntity> _collection;
    private const int DefaultPaginationCount = 50;
    
    public TemplateRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<TemplateEntity>("Templates");
    }

    public async Task<TemplateEntity> CreateTemplate(TemplateEntity template, CancellationToken cancellationToken)
    {
        if (await _collection.Find(t => t.Title == template.Title).AnyAsync(cancellationToken))
        {
            throw new InvalidOperationException($"Template with title '{template.Title}' already exists.");
        }

        await _collection.InsertOneAsync(template, cancellationToken: cancellationToken);
        return template;
    }

    public async Task<TemplateEntity> GetTemplateById(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<TemplateEntity>.Filter.Eq(t => t.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
    
    public async Task<TemplateEntity?> GetTemplateByAnchor(Guid deliveryOwnerId, Guid serviceLineId, Guid marketOfferingId, CancellationToken cancellationToken)
    {
        var filter = Builders<TemplateEntity>.Filter.Eq(t => t.AnchorPoint.DeliveryOwnerId, deliveryOwnerId) &
                     Builders<TemplateEntity>.Filter.Eq(t => t.AnchorPoint.ServiceLineId, serviceLineId) &
                     Builders<TemplateEntity>.Filter.Eq(t => t.AnchorPoint.MarketOfferingId, marketOfferingId);

        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }
    
    public async Task<TemplateEntity> UpdateTemplate(TemplateEntity entity, CancellationToken cancellationToken)
    {
        var entityExists = Builders<TemplateEntity>.Filter.Ne(t => t.Id, entity.Id);

        var exist = await _collection.Find(entityExists).FirstOrDefaultAsync(cancellationToken: cancellationToken);
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
            }, cancellationToken);

        return updated;
    }
    
    public async Task<bool> DeleteTemplate(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<TemplateEntity>.Filter.Eq(t => t.Id, id);
        var result = await _collection.DeleteOneAsync(filter, cancellationToken);
        return result.DeletedCount > 0;
    }
    
    public async Task<List<TemplateEntity>> GetTemplatesByAnchorPointAsync(
        IReadOnlyList<Guid> deliveryOwnerIds, 
        IReadOnlyList<Guid> marketOfferingIds, 
        IReadOnlyList<Guid> serviceLineIds,
        IReadOnlyList<string> deliveryOwnerNames,
        IReadOnlyList<string> marketOfferingNames,
        IReadOnlyList<string> serviceLineNames,
        CancellationToken cancellationToken)
    {
        var filterBuilder = Builders<TemplateEntity>.Filter;
        var filters = new List<FilterDefinition<TemplateEntity>>();

        var allEmpty = deliveryOwnerIds.Count == 0 &&
                        serviceLineIds.Count == 0 &&
                        marketOfferingIds.Count == 0 &&
                        deliveryOwnerNames.Count == 0 &&
                        serviceLineNames.Count == 0 &&
                        marketOfferingNames.Count == 0;

        if (allEmpty)
        {
            return await _collection.Find(filterBuilder.Empty).ToListAsync(cancellationToken);
        }

        AddInFilter(filters, filterBuilder, t => t.AnchorPoint.DeliveryOwnerId, deliveryOwnerIds);
        AddInFilter(filters, filterBuilder, "AnchorPoint.DeliveryOwnerName", deliveryOwnerNames);
        AddInFilter(filters, filterBuilder, t => t.AnchorPoint.ServiceLineId, serviceLineIds);
        AddInFilter(filters, filterBuilder, "AnchorPoint.ServiceLineName", serviceLineNames);
        AddInFilter(filters, filterBuilder, t => t.AnchorPoint.MarketOfferingId, marketOfferingIds);
        AddInFilter(filters, filterBuilder, "AnchorPoint.MarketOfferingName", marketOfferingNames);

        var finalFilter = filters.Count != 0 ? filterBuilder.And(filters) : filterBuilder.Empty;
        return await _collection.Find(finalFilter).ToListAsync(cancellationToken);
    }

    private static void AddInFilter<TField>(
        List<FilterDefinition<TemplateEntity>> filters,
        FilterDefinitionBuilder<TemplateEntity> builder,
        Expression<Func<TemplateEntity, TField>> field,
        IReadOnlyList<TField> values)
    {
        if (values.Count != 0)
        {
            filters.Add(builder.In(field, values));
        }
    }

    private static void AddInFilter(
        List<FilterDefinition<TemplateEntity>> filters,
        FilterDefinitionBuilder<TemplateEntity> builder,
        string fieldPath,
        IReadOnlyList<string> values)
    {
        if (values.Count != 0)
        {
            filters.Add(builder.In(fieldPath, values));
        }
    }
    
    public async Task<List<TemplateEntity>> SearchTemplates(string? searchTerm, int? count, int? page, string? sortColumn, string? ortOrder, CancellationToken cancellationToken)
    {
        var filter = BuildSearchFilter(searchTerm);
        var sort = BuildSortDefinition(sortColumn, ortOrder);
        var (skip, limit) = GetPagination(page, count);
        
        return await _collection
            .Find(filter)
            .Sort(sort)
            .Skip(skip)
            .Limit(limit)
            .ToListAsync(cancellationToken);
    }
    
    private static FilterDefinition<TemplateEntity> BuildSearchFilter(string? searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Builders<TemplateEntity>.Filter.Empty;
        }

        var term = Regex.Escape(searchTerm.Trim());
        var regex = new BsonRegularExpression(term, "i");
        
        var filterBuilder = Builders<TemplateEntity>.Filter;

        var filters = new List<FilterDefinition<TemplateEntity>>
        {
            filterBuilder.Regex(t => t.Title, regex),
            filterBuilder.Regex("AnchorPoint.DeliveryOwnerName", regex),
            filterBuilder.Regex("AnchorPoint.ServiceLineName", regex),
            filterBuilder.Regex("AnchorPoint.MarketOfferingName", regex),
            filterBuilder.ElemMatch(t => t.Attributes, Builders<TemplateAttributeEntity>.Filter.Regex(a => a.AttributeName, regex)),
            filterBuilder.ElemMatch(t => t.Attributes, Builders<TemplateAttributeEntity>.Filter.Regex(a => a.AttributeDescription, regex))
        };

        return filterBuilder.Or(filters);
    }

    private static SortDefinition<TemplateEntity> BuildSortDefinition(string? sortColumn ,string? sortOrder)
    {
        var sortColumnValue = (sortColumn ?? "title").Trim().ToLowerInvariant();
        var sortOrderValue = (sortOrder ?? "asc").Trim().ToLowerInvariant();
        var builder = Builders<TemplateEntity>.Sort;

        return sortColumnValue switch
        {
            "deliveryowner" or "deliveryownername" =>
                sortOrderValue == "desc"
                    ? builder.Descending("AnchorPoint.DeliveryOwnerName")
                    : builder.Ascending("AnchorPoint.DeliveryOwnerName"),

            "serviceline" or "servicelinename" =>
                sortOrderValue == "desc"
                    ? builder.Descending("AnchorPoint.ServiceLineName")
                    : builder.Ascending("AnchorPoint.ServiceLineName"),

            "marketoffering" or "marketofferingname" =>
                sortOrderValue == "desc"
                    ? builder.Descending("AnchorPoint.MarketOfferingName")
                    : builder.Ascending("AnchorPoint.MarketOfferingName"),

            _ =>
                sortOrderValue == "desc"
                    ? builder.Descending(t => t.Title)
                    : builder.Ascending(t => t.Title)
        };
    }

    private static (int skip, int limit) GetPagination(int? page, int? count)
    {
        var pageValue = Math.Max(page ?? 1, 1);
        var countValue = Math.Max(count ?? DefaultPaginationCount, 1);
        return ((pageValue - 1) * countValue, countValue);
    }
}