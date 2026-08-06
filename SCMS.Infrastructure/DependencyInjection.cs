using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using scms.Infrastructure.Extensions;

namespace scms.Infrastructure;

public static class DependencyInjection
{
    // Assembly name used by EF Core migration tooling when targeting TenantDbContext.
    private static readonly string AssemblyName =
        typeof(DependencyInjection).Assembly.GetName().Name!;

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCustomScmsDbContext(configuration);

        // TenantDbContext — per-request connection resolved via ITenantContext (to be enabled
        // after multi-tenant middleware is implemented).  Falls back to TENANT_DESIGN_CONN
        // for EF design-time tooling (dotnet ef migrations add …).
        services.AddTenantDbContext(AssemblyName);

        services.AddRepositories();
        services.AddJwtAuthentication(configuration);
        services.AddAuthServices();
        return services;
    }
}