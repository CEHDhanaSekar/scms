using Microsoft.Extensions.Caching.Memory;
using scms.Application.Dtos;
using scms.Application.Interfaces;

namespace scms.Application.Services;

/// <summary>
/// Resolves a tenant by origin URL with a 5-minute in-memory cache to avoid
/// a DB hit on every Angular app bootstrap.
/// Normalization: trim whitespace, strip trailing slash, lowercase.
/// Matching: exact match against Tenant.DomainUrl after identical normalization.
/// </summary>
public sealed class TenantResolveService : ITenantResolveService
{
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);
    private const string CacheKeyPrefix = "tenantresolve::";

    private readonly ITenantRepository _tenantRepository;
    private readonly IMemoryCache _cache;

    public TenantResolveService(ITenantRepository tenantRepository, IMemoryCache cache)
    {
        _tenantRepository = tenantRepository;
        _cache = cache;
    }

    public async Task<TenantResolveDto?> ResolveByOriginAsync(
        string originUrl,
        CancellationToken ct = default)
    {
        var normalized = Normalize(originUrl);

        if (string.IsNullOrEmpty(normalized))
            return null;

        var cacheKey = CacheKeyPrefix + normalized;

        // Cache hit
        if (_cache.TryGetValue(cacheKey, out TenantResolveDto? cached))
            return cached;

        var tenant = await _tenantRepository.FindByDomainUrlAsync(normalized, ct);

        if (tenant is null)
            return null;

        var dto = new TenantResolveDto
        {
            TenantCode = tenant.TenantCode,
            Name = tenant.Name,
            LogoUrl = tenant.LogoUrl,
            DomainUrl = tenant.DomainUrl
        };

        _cache.Set(cacheKey, dto, CacheDuration);

        return dto;
    }

    /// <summary>Strips trailing slash, lowercases, trims.</summary>
    internal static string Normalize(string url) =>
        url.Trim().TrimEnd('/').ToLowerInvariant();
}
