using SCMS.Domain.Enums;

namespace scms.Application.Common.Caching;

public interface ICacheService
{
    // ── Low-level primitives ──────────────────────────────────────────────────

    Task<T?> GetAsync<T>(
        string key,
        CancellationToken cancellationToken = default);

    Task SetAsync<T>(
        string key,
        T value,
        TimeSpan expiration,
        CancellationToken cancellationToken = default);

    Task RemoveAsync(
        string key,
        CancellationToken cancellationToken = default);

    // ── Cache-aside helpers ───────────────────────────────────────────────────

    /// <summary>
    /// Cache-aside for app-wide (non-tenant) data.
    /// Key format: <c>{entity}:{key}</c>
    /// Returns cached value if present; otherwise calls <paramref name="factory"/>,
    /// stores the result, and returns it.
    /// </summary>
    Task<T?> GetAsync<T>(
        CacheEntity entity,
        string key,
        Func<Task<T>> factory,
        TimeSpan expiration,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Cache-aside scoped to the current tenant (resolved from <c>ITenantContext</c>).
    /// Key format: <c>T:{TENANT}:{entity}:{key}</c>
    /// Returns cached value if present; otherwise calls <paramref name="factory"/>,
    /// stores the result, and returns it.
    /// </summary>
    Task<T?> GetTenantAsync<T>(
        CacheEntity entity,
        string key,
        Func<Task<T>> factory,
        TimeSpan expiration,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes a tenant-scoped cache entry.
    /// Key format: <c>T:{TENANT}:{entity}:{key}</c>
    /// </summary>
    Task RemoveTenantAsync(
        CacheEntity entity,
        string key,
        CancellationToken cancellationToken = default);
}