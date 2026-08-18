namespace scms.Application.Interfaces;

/// <summary>
/// Safely applies all pending EF Core Tenant migrations to a specific
/// tenant's database identified by its connection string.
/// </summary>
public interface ITenantMigrator
{
    /// <summary>
    /// Resolves the connection string for <paramref name="tenantCode"/>,
    /// ensures the migration-history log table exists, applies any pending
    /// migrations, and returns a detailed result record.
    /// </summary>
    Task<MigrationResult> MigrateAsync(string tenantCode, CancellationToken ct = default);
}

public sealed record MigrationResult(
    bool Success,
    string TenantCode,
    string ConnectionString,
    IReadOnlyList<string> AppliedMigrations,
    IReadOnlyList<string> PendingMigrations,
    Exception? Error = null);
