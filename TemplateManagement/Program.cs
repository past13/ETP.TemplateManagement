using FluentValidation;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TemplateManagement;
using TemplateManagement.Domain.Entities;
using TemplateManagement.Handlers;
using TemplateManagement.Repositories;
using TemplateManagement.Validators;

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

builder.Services.AddAutoMapper(cfg => {}, typeof(AutoMapperProfile).Assembly);

builder.Services.AddValidatorsFromAssembly(typeof(CreateTemplateCommandValidator).Assembly);
builder.Services.AddValidatorsFromAssembly(typeof(UpdateTemplateCommandValidator).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateTemplateHandler).Assembly));

builder.Services.AddTransient<ITemplateRepository, TemplateRepository>();

builder.Services.AddControllers();

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
