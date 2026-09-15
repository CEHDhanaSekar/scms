namespace scms.Infrastructure.Caching;

/// <summary>
/// Centralized cache key segment constants to avoid magic strings and
/// prevent case-sensitivity mismatches across controllers.
/// </summary>
public static class CacheKeys
{
    /// <summary>all:active=true</summary>
    public const string AllActive = "all:active=true";

    /// <summary>all:active=false</summary>
    public const string AllInactive = "all:active=false";

    /// <summary>
    /// Returns the correct <c>all:active={value}</c> key segment for
    /// a given boolean, always in lowercase so it is consistent with
    /// <see cref="AllActive"/> and <see cref="AllInactive"/>.
    /// </summary>
    public static string AllActiveKey(bool onlyActive)
        => onlyActive ? AllActive : AllInactive;
}
