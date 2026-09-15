using Microsoft.Extensions.Caching.Distributed;
using scms.Application.Common.Caching;
using System.Text.Json;

namespace scms.Infrastructure.Caching;

public sealed class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    // ── Cache-aside ───────────────────────────────────────────────────────────

    public async Task<T?> GetOrSetAsync<T>(
        string key,
        Func<Task<T>> factory,
        TimeSpan expiration,
        CancellationToken cancellationToken = default)
    {
        var json = await _cache.GetStringAsync(key, cancellationToken);

        if (json != null)
            return JsonSerializer.Deserialize<T>(json);

        var data = await factory();

        await SetAsync(key, data, expiration, cancellationToken);

        return data;
    }

    // ── Low-level primitives ──────────────────────────────────────────────────

    private async Task SetAsync<T>(
        string key,
        T value,
        TimeSpan expiration,
        CancellationToken cancellationToken = default)
    {
        var json = JsonSerializer.Serialize(value);

        await _cache.SetStringAsync(
            key,
            json,
            new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration
            },
            cancellationToken);
    }

    public Task RemoveAsync(
        string key,
        CancellationToken cancellationToken = default)
        => _cache.RemoveAsync(key, cancellationToken);

}
