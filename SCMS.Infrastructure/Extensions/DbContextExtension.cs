using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using scms.Infrastructure.Data;
using scms.Infrastructure.Persistence;

namespace scms.Infrastructure.Extensions;

public static class DbContextExtension
{
    // Both contexts live in the same infrastructure assembly; the Migrations/SCMS and
    // Migrations/Tenant sub-folders are distinguished by C# namespace convention.
    private static readonly string InfraAssembly =
        typeof(DbContextExtension).Assembly.GetName().Name!;

    /// <summary>
    /// Registers <see cref="ScmsDbContext"/> (owner / platform database).
    /// <para>
    /// Migrations are stored in <c>Migrations/SCMS/</c>. To add a new migration run:
    /// <code>
    /// dotnet ef migrations add &lt;Name&gt; --context ScmsDbContext --output-dir Migrations/SCMS --project SCMS.Infrastructure --startup-project SCMS.API
    /// </code>
    /// </para>
    /// </summary>
    public static IServiceCollection AddCustomScmsDbContext(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["SCMS_DEFAULT_CONNECTION"];

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "SCMS_DEFAULT_CONNECTION is not configured.");
        }

        services.AddDbContext<ScmsDbContext>(options =>
        {
            Configure(options, connectionString, InfraAssembly, "__ef_migrations_history_scms");
        });

        return services;
    }

    /// <summary>
    /// Registers tenant-scoped <see cref="TenantDbContext"/> using per-request connection strings.
    /// <para>
    /// At runtime the connection string is resolved from <c>ITenantContext</c>
    /// (injected by TenantResolverMiddleware — to be wired up during multi-tenant implementation).
    /// </para>
    /// <para>
    /// Migrations are stored in <c>Migrations/Tenant/</c>. To add a new migration run:
    /// <code>
    /// dotnet ef migrations add &lt;Name&gt; --context TenantDbContext --output-dir Migrations/Tenant --project SCMS.Infrastructure --startup-project SCMS.API
    /// </code>
    /// For design-time tooling set <c>TENANT_DESIGN_CONN</c> (env var or config key).
    /// </para>
    /// </summary>
    public static IServiceCollection AddTenantDbContext(
        this IServiceCollection services,
        string? migrationAssembly = null)
    {
        return services.AddDbContext<TenantDbContext>((sp, opt) =>
        {
            // ── Runtime path ──────────────────────────────────────────────────
            // TODO: Uncomment once ITenantContext / TenantResolverMiddleware is implemented.
            // var tenant = sp.GetService<ITenantContext>();
            // var cs = tenant?.ConnectionString;
            // if (!string.IsNullOrWhiteSpace(cs))
            // {
            //     Configure(opt, cs, migrationAssembly, "__ef_migrations_history_tenant");
            //     return;
            // }

            // ── Design-time / migration fallback ─────────────────────────────
            var cfg = sp.GetService<IConfiguration>();
            var designCs =
                cfg?["TENANT_DESIGN_CONN"] ??
                Environment.GetEnvironmentVariable("TENANT_DESIGN_CONN");

            if (!string.IsNullOrWhiteSpace(designCs))
            {
                Configure(opt, designCs!, migrationAssembly, "__ef_migrations_history_tenant");
                return;
            }

            throw new InvalidOperationException(
                "Tenant connection not resolved. " +
                "At runtime ensure TenantResolverMiddleware ran (so ITenantContext is set). " +
                "For migrations/design-time, set TENANT_DESIGN_CONN.");
        });
    }

    private static void Configure(
        DbContextOptionsBuilder opt,
        string conn,
        string? migrationAssembly,
        string historyTable)
    {
        opt.UseNpgsql(conn, b =>
        {
            b.MigrationsAssembly(migrationAssembly ?? InfraAssembly);
            b.MigrationsHistoryTable(historyTable);
            b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
        })
        .UseSnakeCaseNamingConvention();
    }
}
