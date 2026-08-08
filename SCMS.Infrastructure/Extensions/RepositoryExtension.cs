using Microsoft.Extensions.DependencyInjection;
using scms.Application.Interfaces;
using scms.Infrastructure.Repositories.SCMS;

namespace scms.Infrastructure.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<IModulePermissionRepository, ModulePermissionRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IPlanModuleRepository, PlanModuleRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();
        return services;
    }
}

