using Microsoft.AspNetCore.Http;
using scms.Shared.Models;
using System.Text.Json;

namespace scms.Shared.Middlewares;

/// <summary>
/// Extracts <c>x-tenant-code</c> from the request header and calls
/// <see cref="ITenantContext.ResolveAsync"/> to populate tenant state for the request.
///
/// Skip paths:
///   - /api/owner/   (owner/master routes — no tenant needed)
///   - /api/tenant/v1/resolve  (provides the tenant code itself)
///   - /openapi, /scalar       (tooling)
/// </summary>
public class TenantResolverMiddleware
{
    private readonly RequestDelegate _next;

    public TenantResolverMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ITenantContext tenantContext)
    {
        var path = context.Request.Path.Value?.ToLowerInvariant() ?? string.Empty;

        if (ShouldSkip(path))
        {
            await _next(context);
            return;
        }

        var tenantCode = context.Request.Headers["x-tenant-code"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(tenantCode))
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";
            var error = new
            {
                Success = false,
                StatusCode = 400,
                Message = "Missing required header: x-tenant-code"
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(error));
            return;
        }

        await tenantContext.ResolveAsync(tenantCode.Trim(), context.RequestAborted);
        await _next(context);
    }

    private static bool ShouldSkip(string path) =>
        path.StartsWith("/api/owner/", StringComparison.Ordinal) ||
        path.StartsWith("/api/tenant/v1/resolve", StringComparison.Ordinal) ||
        path.StartsWith("/openapi", StringComparison.Ordinal) ||
        path.StartsWith("/scalar", StringComparison.Ordinal);
}
