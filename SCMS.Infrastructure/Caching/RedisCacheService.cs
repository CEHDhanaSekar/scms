using Microsoft.Extensions.Caching.Distributed;
using scms.Application.Common.Caching;
using scms.Shared.Models;
using SCMS.Domain.Enums;
using System.Text.Json;

namespace scms.Infrastructure.Caching;

public sealed class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly ITenantContext _tenantContext;

    public RedisCacheService(
        IDistributedCache cache,
        ITenantContext tenantContext)
    {
        _cache = cache;
        _tenantContext = tenantContext;
    }

    // ── Low-level primitives ──────────────────────────────────────────────────

    public async Task<T?> GetAsync<T>(
        string key,
        CancellationToken cancellationToken = default)
    {
        var json = await _cache.GetStringAsync(key, cancellationToken);

        if (string.IsNullOrWhiteSpace(json) || json == "null")
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
        => _cache.RemoveAsync(key, cancellationToken);

    // ── Cache-aside: app-wide ─────────────────────────────────────────────────

    /// <inheritdoc />
    public async Task<T?> GetAsync<T>(
        CacheEntity entity,
        string key,
        Func<Task<T>> factory,
        TimeSpan expiration,
        CancellationToken cancellationToken = default)
    {
        var fullKey = BuildGlobalKey(entity, key);
        return await GetOrSetAsync(fullKey, factory, expiration, cancellationToken);
    }

    // ── Cache-aside: tenant-scoped ────────────────────────────────────────────

    /// <inheritdoc />
    public async Task<T?> GetTenantAsync<T>(
        CacheEntity entity,
        string key,
        Func<Task<T>> factory,
        TimeSpan expiration,
        CancellationToken cancellationToken = default)
    {
        var fullKey = BuildTenantKey(entity, key);
        return await GetOrSetAsync(fullKey, factory, expiration, cancellationToken);
    }

    /// <inheritdoc />
    public Task RemoveTenantAsync(
        CacheEntity entity,
        string key,
        CancellationToken cancellationToken = default)
        => _cache.RemoveAsync(BuildTenantKey(entity, key), cancellationToken);

    // ── Core cache-aside logic ────────────────────────────────────────────────

    private async Task<T?> GetOrSetAsync<T>(
        string fullKey,
        Func<Task<T>> factory,
        TimeSpan expiration,
        CancellationToken cancellationToken)
    {
        var cached = await _cache.GetStringAsync(fullKey, cancellationToken);

        // Cache hit — try to deserialize
        if (!string.IsNullOrWhiteSpace(cached) && cached != "null")
        {
            try
            {
                return JsonSerializer.Deserialize<T>(cached);
            }
            catch
            {
                // Corrupted entry — fall through and refresh below
            }
        }

        // Cache miss (or corrupted) — call the factory
        var value = await factory();

        if (value is not null)
        {
            await _cache.SetStringAsync(
                fullKey,
                JsonSerializer.Serialize(value),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration
                },
                cancellationToken);
        }

        return value;
    }

    // ── Key builders ──────────────────────────────────────────────────────────

    /// <summary>App-wide key — <c>{entity}:{key}</c></summary>
    private static string BuildGlobalKey(CacheEntity entity, string key)
        => $"{entity}:{key}";

    /// <summary>Tenant-scoped key — <c>T:{TENANT}:{entity}:{key}</c></summary>
    private string BuildTenantKey(CacheEntity entity, string key)
        => $"T:{_tenantContext.TenantCode.ToUpperInvariant()}:{entity}:{key}";
}
