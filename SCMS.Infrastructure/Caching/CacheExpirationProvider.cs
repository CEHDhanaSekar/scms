using Microsoft.Extensions.Options;

namespace scms.Infrastructure.Caching;

public sealed class CacheExpirationProvider
{
    private readonly CacheOptions _options;

    public CacheExpirationProvider(
        IOptions<CacheOptions> options)
    {
        _options = options.Value;
    }

    public TimeSpan GetExpiration()
    {
        var jitter = Random.Shared.Next(
            0,
            _options.JitterMinutes + 1);

        return TimeSpan.FromMinutes(
            _options.DefaultTtlMinutes + jitter);
    }
}