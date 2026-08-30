using System.Net;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try { await next(context); }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled API exception");
            var (status, title) = ex switch
            {
                ValidationException => ((int)HttpStatusCode.BadRequest, "Validation failed"),
                UnauthorizedAccessException => ((int)HttpStatusCode.Unauthorized, "Unauthorized"),
                KeyNotFoundException => ((int)HttpStatusCode.NotFound, "Not found"),
                InvalidOperationException => ((int)HttpStatusCode.Conflict, "Operation rejected"),
                _ => ((int)HttpStatusCode.InternalServerError, "Unexpected error")
            };
            context.Response.StatusCode = status;
            await context.Response.WriteAsJsonAsync(new ProblemDetails
            {
                Status = status,
                Title = title,
                Detail = status == 500 ? "حدث خطأ غير متوقع." : ex.Message,
                Instance = context.Request.Path
            });
        }
    }
}
