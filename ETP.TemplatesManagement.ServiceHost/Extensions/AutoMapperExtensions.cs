using ETP.TemplatesManagement.ServiceHost.Mappers;

namespace ETP.TemplatesManagement.ServiceHost.Extensions;

public static class AutoMapperExtensions
{
    public static IServiceCollection AddApplicationAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(_ => { }, typeof(AnchorPointMapper).Assembly);
        services.AddAutoMapper(_ => { }, typeof(AttributeMapper).Assembly);
        services.AddAutoMapper(_ => { }, typeof(TemplateMapper).Assembly);
        return services;
    }
}