using System.Text.Json;
using System.Text.Json.Serialization;
using ETP.TemplatesManagement.Data;
using ETP.TemplatesManagement.Data.Entities;
using ETP.TemplatesManagement.RA.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ETP.TemplatesManagement.ServiceHost;
using ETP.TemplatesManagement.ServiceHost.Handlers;
using ETP.TemplatesManagement.ServiceHost.Mappers;
using ETP.TemplatesManagement.ServiceHost.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

builder.Services.AddAutoMapper(cfg => {}, typeof(AnchorPointMapper).Assembly);
builder.Services.AddAutoMapper(cfg => {}, typeof(AttributeMapper).Assembly);
builder.Services.AddAutoMapper(cfg => {}, typeof(TemplateMapper).Assembly);

builder.Services.AddValidatorsFromAssembly(typeof(CreateTemplateCommandValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(QueryTemplateCommandValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(UpdateTemplateCommandValidator).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTemplateHandler).Assembly));

builder.Services.AddTransient<ITemplateRepository, TemplateRepository>();

builder.Services.AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
        opt.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// var database = app.Services.GetRequiredService<IMongoDatabase>();
// MongoDbInitializer.EnsureIndexes(database);

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
