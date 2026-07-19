using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using scms.Infrastructure.Data;

namespace scms.Infrastructure.Extensions;

public static class DbContextExtension
{
    public static IServiceCollection AddCustomScmsDbContext(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["SCMS_DEFAULT_CONNECTION"];

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
}
