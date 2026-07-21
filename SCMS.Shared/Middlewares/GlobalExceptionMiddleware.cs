using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using scms.Shared.Exceptions.Abstract;
using scms.Shared.Models;
using SCMS.Shared.Exceptions;
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
        catch (KeyNotFoundException ex)
        {
            await HandleBaseExceptionAsync(context, new NotFoundException(ex.Message));
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleBaseExceptionAsync(context, new UnauthorizedException(ex.Message));
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

        var errors = (exception as ValidationException)?.Errors;

        var response = new ErrorResponse
        {
            StatusCode = (int)exception.StatusCode,
            Message = exception.Message,
            Errors = errors
        };
        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, exception.Message);

        var statusCode = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,
            TimeoutException => StatusCodes.Status408RequestTimeout,
            NotImplementedException => StatusCodes.Status501NotImplemented,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = new ErrorResponse
        {
            StatusCode = statusCode,
            Message = exception.Message,
            Errors = new List<string> { exception.GetType().Name }
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}
