using Microsoft.Extensions.Options;
using scms.Application.Interfaces;
using scms.Shared.Models;

namespace scms.Infrastructure.Caching;

/// <summary>
/// Builds deterministic, tenant-scoped Redis cache keys.
/// Registered as <b>Scoped</b> because it depends on <see cref="ITenantContext"/>,
/// which is populated per-request by <c>TenantResolverMiddleware</c>.
/// Key format: <c>{instanceName}{tenantCode}:{entity}:{key}</c>
/// </summary>
public sealed class CacheKeyFactory : ICacheKeyFactory
{
    private readonly CacheOptions _options;
    private readonly ITenantContext _tenantContext;

    public CacheKeyFactory(
        IOptions<CacheOptions> options,
        ITenantContext tenantContext)
    {
        _options = options.Value;
        _tenantContext = tenantContext;
    }

    /// <inheritdoc />
    public string Create(string entity, object key)
    {
        return $"{_options.InstanceName}" +
               $"{_tenantContext.TenantCode}:" +
               $"{entity}:" +
               $"{key}";
    }
}
