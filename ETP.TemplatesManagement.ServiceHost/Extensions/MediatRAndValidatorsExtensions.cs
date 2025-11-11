using ETP.TemplatesManagement.ServiceHost.Handlers;
using ETP.TemplatesManagement.ServiceHost.Validators;
using FluentValidation;
using MediatR;

namespace ETP.TemplatesManagement.ServiceHost.Extensions;

public static class MediatRAndValidatorsExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // MediatR handlers
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTemplateHandler).Assembly));

        // FluentValidation validators
        services.AddValidatorsFromAssembly(typeof(CreateTemplateCommandValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(QueryTemplateCommandValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(UpdateTemplateCommandValidator).Assembly);
        services.AddValidatorsFromAssembly(typeof(QueryTemplatesCommandValidator).Assembly);

        // Pipeline behaviors
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}