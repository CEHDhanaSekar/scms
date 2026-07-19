using Microsoft.Extensions.DependencyInjection;

namespace scms.Infrastructure.Extensions;

public static class RepositoryExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<Application.Interfaces.IModuleRepository, Repositories.ModuleRepository>();
        return services;
    }
}
