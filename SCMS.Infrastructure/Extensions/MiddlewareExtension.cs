using Microsoft.AspNetCore.Builder;
using scms.Shared.Middlewares;

namespace scms.Infrastructure.Extensions;

public static class MiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandler(
        this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionMiddleware>();
    }
}
