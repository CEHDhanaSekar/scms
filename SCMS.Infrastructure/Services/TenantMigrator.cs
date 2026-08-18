using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using scms.Application.Interfaces;
using scms.Infrastructure.Persistence;

namespace scms.Infrastructure.Services;

public class TenantMigrator : ITenantMigrator
{
    private readonly IConfiguration _config;
    private readonly ILogger<TenantMigrator> _logger;

    public TenantMigrator(IConfiguration config, ILogger<TenantMigrator> logger)
    {
        _config = config;
        _logger = logger;
    }

    public async Task<MigrationResult> MigrateAsync(string tenantCode, CancellationToken ct = default)
    {
        var normalized = tenantCode.Trim().ToLowerInvariant();
        var envCode = normalized.ToUpperInvariant().Replace("-", "_");
        var envVar = $"SCMS_{envCode}_SHARED_CONNECTION";
        
        var connStr = Environment.GetEnvironmentVariable(envVar) ?? _config[envVar];

        if (string.IsNullOrWhiteSpace(connStr))
        {
            var err = new InvalidOperationException($"Connection string for tenant '{normalized}' not found (expected env var or config key: {envVar}).");
            return new MigrationResult(false, normalized, string.Empty, Array.Empty<string>(), Array.Empty<string>(), err);
        }

        var assemblyName = typeof(DependencyInjection).Assembly.GetName().Name!;

        var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
        optionsBuilder.UseNpgsql(connStr, b =>
        {
            b.MigrationsAssembly(assemblyName);
            b.MigrationsHistoryTable("__ef_migrations_history_tenant");
            b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
        }).UseSnakeCaseNamingConvention();

        using var ctx = new TenantDbContext(optionsBuilder.Options);

        var pending = new List<string>();
        try
        {
            var pendingEnum = await ctx.Database.GetPendingMigrationsAsync(ct);
            pending = pendingEnum.ToList();
            
            if (pending.Count == 0)
            {
                _logger.LogInformation("No pending migrations for tenant '{TenantCode}'.", normalized);
                return new MigrationResult(true, normalized, connStr, Array.Empty<string>(), pending);
            }

            _logger.LogInformation("Applying {Count} migrations for tenant '{TenantCode}'...", pending.Count, normalized);
            
            // MigrateAsync automatically ensures the history table exists
            await ctx.Database.MigrateAsync(ct);

            var appliedEnum = await ctx.Database.GetAppliedMigrationsAsync(ct);
            var applied = appliedEnum.ToList();

            _logger.LogInformation("Successfully applied migrations for tenant '{TenantCode}'.", normalized);
            
            return new MigrationResult(true, normalized, connStr, applied, pending);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to apply migrations for tenant '{TenantCode}'.", normalized);
            return new MigrationResult(false, normalized, connStr, Array.Empty<string>(), pending, ex);
        }
    }
}
