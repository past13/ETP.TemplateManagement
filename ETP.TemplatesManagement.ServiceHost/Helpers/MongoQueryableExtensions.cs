namespace ETP.TemplatesManagement.ServiceHost.Helpers;

public static class MongoQueryableExtensions
{
    public static IQueryable<T> ApplySorting<T>(
        this IQueryable<T> query,
        string? sortBy,
        string? sortDirection)
    {
        if (string.IsNullOrEmpty(sortBy))
            return query;

        var prop = typeof(T).GetProperty(sortBy);
        if (prop == null)
            return query;

        return (sortDirection?.ToLower()) switch
        {
            "desc" => query.OrderByDescending(x => prop.GetValue(x, null)),
            _ => query.OrderBy(x => prop.GetValue(x, null))
        };
    }

    public static IQueryable<T> ApplyPaging<T>(
        this IQueryable<T> query,
        int page,
        int pageSize)
    {
        return query.Skip((page - 1) * pageSize).Take(pageSize);
    }
}