using Microsoft.Extensions.DependencyInjection;
using scms.Application.Interfaces.Tenant;
using scms.Application.Services;
using scms.Application.Services.SCMS;
using scms.Application.Services.Tenant;

namespace scms.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => {}, typeof(Mapper.MappingProfile).Assembly, typeof(Mapper.TenantMappingProfile).Assembly);
        
        // SCMS Services
        services.AddScoped<IModuleService, ModuleService>();
        services.AddScoped<IModulePermissionService, ModulePermissionService>();
        services.AddScoped<scms.Application.Services.SCMS.IPermissionService, scms.Application.Services.SCMS.PermissionService>();
        services.AddScoped<IPlanService, PlanService>();
        services.AddScoped<IPlanModuleService, PlanModuleService>();
        services.AddScoped<ITenantResolveService, TenantResolveService>();
        services.AddScoped<ITenantOnboardingService, TenantOnboardingService>();
        services.AddScoped<ITenantService, TenantService>();
        
        // Tenant Services
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ITenantPermissionService, TenantPermissionService>();

        // Validators
        // FluentValidation automatically scanned in API layer usually, but we can register if needed.
        return services;
    }
}
