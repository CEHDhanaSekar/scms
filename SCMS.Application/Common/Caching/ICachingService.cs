namespace scms.Application.Common.Caching;

public interface ICacheService
{
    /// <summary>
    /// Returns the cached value for <paramref name="key"/>.
    /// On a cache miss, <paramref name="factory"/> is invoked, the result is stored
    /// with <paramref name="expiration"/>, and then returned.
    /// </summary>
    Task<T?> GetOrSetAsync<T>(
        string key,
        Func<Task<T>> factory,
        TimeSpan expiration,
        CancellationToken cancellationToken = default);

    /// <summary>Removes the entry with the given <paramref name="key"/> from the cache.</summary>
    Task RemoveAsync(
        string key,
        CancellationToken cancellationToken = default);
}
