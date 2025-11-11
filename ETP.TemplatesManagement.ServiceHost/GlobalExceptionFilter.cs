using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MongoDB.Driver;

namespace ETP.TemplatesManagement.ServiceHost;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var actionName = context.ActionDescriptor.DisplayName;
        _logger.LogError(context.Exception, $"Unhandled exception occurred: {actionName}");

        var statusCode = context.Exception switch
        {
            ValidationException or ArgumentException or InvalidOperationException or InvalidDataException => HttpStatusCode.BadRequest,
            
            UnauthorizedAccessException => HttpStatusCode.Forbidden,
            
            KeyNotFoundException => HttpStatusCode.NotFound,
            
            MongoWriteException mongoEx when IsDuplicateKeyError(mongoEx) => HttpStatusCode.BadRequest,
            MongoWriteException => HttpStatusCode.InternalServerError,

            HttpRequestException httpEx => httpEx.StatusCode ?? HttpStatusCode.InternalServerError,

            TaskCanceledException or OperationCanceledException => HttpStatusCode.ServiceUnavailable,

            _ => HttpStatusCode.InternalServerError
        };

        context.Result = new JsonResult(new
        {
            Error = context.Exception.Message,
            Details = context.Exception.InnerException?.Message
        })
        {
            StatusCode = (int)statusCode
        };
    }

    private static bool IsDuplicateKeyError(MongoWriteException mongoEx)
    {
       return mongoEx.WriteError.Category == ServerErrorCategory.DuplicateKey;
    }
}