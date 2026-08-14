using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using scms.Infrastructure.Extensions;
using scms.Infrastructure.Services;
using scms.Shared.Models;

namespace scms.Infrastructure;

public static class DependencyInjection
{
    // Assembly name used by EF Core migration tooling when targeting TenantDbContext.
    private static readonly string AssemblyName =
        typeof(DependencyInjection).Assembly.GetName().Name!;

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();

        services.AddScoped<ITenantContext, TenantContext>();

        services.AddCustomScmsDbContext(configuration);

        // TenantDbContext — per-request connection resolved via ITenantContext.
        // Falls back to TENANT_DESIGN_CONN for EF design-time tooling.
        services.AddTenantDbContext(AssemblyName);

        services.AddScoped<ITenantMigrator, TenantMigrator>();
        
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.AddScoped<Application.Services.IEmailSender, MailKitEmailSender>();

        services.AddRepositories();
        services.AddJwtAuthentication(configuration);
        services.AddAuthServices();
        return services;
    }
}