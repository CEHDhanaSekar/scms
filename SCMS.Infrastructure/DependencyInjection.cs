using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using scms.Infrastructure.Extensions;

namespace scms.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCustomScmsDbContext(configuration);
        services.AddRepositories();
        return services;
    }
}