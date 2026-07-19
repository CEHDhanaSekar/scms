using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using scms.Infrastructure.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomScmsDbContext(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("SCMS_DEFAULT_CONNECTION");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "SCMS_DEFAULT_CONNECTION is not configured.");
        }

        services.AddDbContext<ScmsDbContext>(options =>
        {
            options.UseNpgsql(connectionString)
                   .UseSnakeCaseNamingConvention();
        });

        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<scms.Application.Interfaces.IModuleRepository, scms.Infrastructure.Repositories.ModuleRepository>();
        services.AddScoped<scms.Application.Services.IModuleService, scms.Application.Services.ModuleService>();
        return services;
    }
}