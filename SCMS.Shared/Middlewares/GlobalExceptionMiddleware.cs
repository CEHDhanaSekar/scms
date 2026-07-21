using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using scms.Shared.Exceptions.Abstract;
using scms.Shared.Models;
using System.Text.Json;

namespace scms.Shared.Middlewares;

public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;

    public GlobalExceptionMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionMiddleware> logger)
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
        catch (BaseException ex)
        {
            await HandleBaseExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleBaseExceptionAsync(HttpContext context, BaseException exception)
    {
        _logger.LogError(exception, exception.Message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)exception.StatusCode;
        var response = new ErrorResponse
        {
            StatusCode = (int)exception.StatusCode,
            Message = exception.Message
        };
        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, exception.Message);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        var response = new ErrorResponse
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = "An unexpected error occurred. Please try again later."
        };
        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}
