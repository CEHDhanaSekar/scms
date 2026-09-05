using Microsoft.Extensions.Options;

namespace scms.Infrastructure.Caching;

public sealed class CacheKeyFactory : ICacheKeyFactory
{
    private readonly CacheOptions _options;

    public CacheKeyFactory(IOptions<CacheOptions> options)
    {
        _options = options.Value;
    }

    public string Create(
        string tenantCode,
        string entity,
        object key)
    {
        return $"{_options.InstanceName}" +
               $"{tenantCode}:" +
               $"{entity}:" +
               $"{key}";
    }
}
