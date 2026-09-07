using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using scms.Application.Common.Caching;
using scms.Application.Interfaces;
using scms.Infrastructure.Caching;

namespace scms.Infrastructure.Extensions;

public static class CacheExtension
{
    public static IServiceCollection AddScmsCaching(
    this IServiceCollection services,
    IConfiguration configuration)
    {
        services.Configure<CacheOptions>(
            configuration.GetSection("Redis"));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration =
                configuration["Redis:ConnectionString"];

            options.InstanceName =
                configuration["Redis:InstanceName"];
        });

        services.AddSingleton<ICacheKeyFactory,
            CacheKeyFactory>();

        services.AddSingleton<CacheExpirationProvider>();

        services.AddScoped<ICacheService,
            RedisCacheService>();

        return services;
    }
}
