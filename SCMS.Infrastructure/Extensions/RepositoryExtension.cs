using Microsoft.Extensions.DependencyInjection;
using scms.Infrastructure.Repositories.SCMS;

namespace scms.Infrastructure.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<Application.Interfaces.IModuleRepository, ModuleRepository>();
        services.AddScoped<Application.Interfaces.IModulePermissionRepository, ModulePermissionRepository>();
        services.AddScoped<Application.Interfaces.IPlanRepository, PlanRepository>();
        services.AddScoped<Application.Interfaces.IPlanModuleRepository, PlanModuleRepository>();
        return services;
    }
}
