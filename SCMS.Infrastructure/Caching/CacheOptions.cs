namespace scms.Infrastructure.Caching;


public sealed class CacheOptions
{
    public string ConnectionString { get; set; } = string.Empty;

    public string InstanceName { get; set; } = "SCMS:";

    public int DefaultTtlMinutes { get; set; } = 30;

    public int JitterMinutes { get; set; } = 5;
}