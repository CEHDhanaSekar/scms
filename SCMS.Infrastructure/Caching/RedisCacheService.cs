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

    public async Task<T?> GetAsync<T>(
        string key,
        CancellationToken cancellationToken = default)
    {
        var json = await _cache.GetStringAsync(
            key,
            cancellationToken);

        if (json is null)
            return default;

        return JsonSerializer.Deserialize<T>(json);
    }

    public async Task SetAsync<T>(
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
    {
        return _cache.RemoveAsync(
            key,
            cancellationToken);
    }
}
