using System;
using DirectoryService.Domain.Common;
using DirectoryService.Domain.Common.Exceptions;

namespace DirectoryService.Web.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception occurred.");

            var (statusCode, errors) = ex switch
{
                DomainValidationException validationException =>
                    (StatusCodes.Status400BadRequest,
                    validationException.Errors.ToArray()),

                DomainException domainException =>
                    (GetStatusCode(domainException.Error.Type),
                    [domainException.Error]),

                _ =>
                    (StatusCodes.Status500InternalServerError,
                    new[]
                    {
                        Error.Internal(
                            "internal.server_error",
                            "Произошла внутренняя ошибка сервера."),
                    })
};

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(errors);
        }
    }

    private static int GetStatusCode(ErrorType type) =>
        type switch
        {
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Internal => StatusCodes.Status500InternalServerError,
            _ => StatusCodes.Status500InternalServerError
        };
}
