using Microsoft.Extensions.DependencyInjection;

namespace scms.Infrastructure.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<Application.Interfaces.IModuleRepository, Repositories.ModuleRepository>();
        services.AddScoped<Application.Interfaces.IModulePermissionRepository, Repositories.ModulePermissionRepository>();
        services.AddScoped<Application.Interfaces.IPlanRepository, Repositories.PlanRepository>();
        services.AddScoped<Application.Interfaces.IPlanModuleRepository, Repositories.PlanModuleRepository>();
        return services;
    }
}
