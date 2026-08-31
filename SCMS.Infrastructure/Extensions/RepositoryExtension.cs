using Microsoft.Extensions.DependencyInjection;
using scms.Application.Interfaces;
using scms.Application.Interfaces.Tenant;
using scms.Infrastructure.Repositories.SCMS;
using scms.Infrastructure.Repositories.Tenant;

namespace scms.Infrastructure.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IModuleRepository, ModuleRepository>();
        services.AddScoped<IModulePermissionRepository, ModulePermissionRepository>();
        services.AddScoped<scms.Application.Interfaces.IPermissionRepository, scms.Infrastructure.Repositories.SCMS.PermissionRepository>();
        services.AddScoped<IPlanRepository, PlanRepository>();
        services.AddScoped<IPlanModuleRepository, PlanModuleRepository>();
        services.AddScoped<ITenantRepository, TenantRepository>();

        // Tenant Repositories
        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<ITenantPermissionRepository, TenantPermissionRepository>();
        services.AddScoped<ISpecializationRepository, SpecializationRepository>();

        return services;
    }
}

