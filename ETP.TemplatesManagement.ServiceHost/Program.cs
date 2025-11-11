using ETP.TemplatesManagement.RA.Repositories;
using ETP.TemplatesManagement.ServiceHost;
using ETP.TemplatesManagement.ServiceHost.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMongoDb(builder.Configuration);

builder.Services.AddApplicationAutoMapper();

builder.Services.AddApplicationServices();

builder.Services.AddTransient<ITemplateRepository, TemplateRepository>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<GlobalExceptionFilter>();
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
