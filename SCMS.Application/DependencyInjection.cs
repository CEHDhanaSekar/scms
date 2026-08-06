using Microsoft.Extensions.DependencyInjection;
using scms.Application.Services;
using scms.Application.Services.SCMS;

namespace scms.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => {}, typeof(Mapper.MappingProfile).Assembly);
        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped<IModulePermissionService, ModulePermissionService>();
        services.AddScoped<IPlanService, PlanService>();
        services.AddScoped<IPlanModuleService, PlanModuleService>();
        services.AddScoped<ITenantResolveService, TenantResolveService>();
        return services;
    }
}
