using Microsoft.Extensions.DependencyInjection;

namespace scms.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<Services.IModuleService, Services.ModuleService>();
        services.AddScoped<Services.IModulePermissionService, Services.ModulePermissionService>();
        services.AddScoped<Services.IPlanService, Services.PlanService>();
        services.AddScoped<Services.IPlanModuleService, Services.PlanModuleService>();
        return services;
    }
}
