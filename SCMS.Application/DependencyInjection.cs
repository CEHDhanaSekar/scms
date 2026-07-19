using Microsoft.Extensions.DependencyInjection;

namespace scms.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<Services.IModuleService, Services.ModuleService>();
        return services;
    }
}
