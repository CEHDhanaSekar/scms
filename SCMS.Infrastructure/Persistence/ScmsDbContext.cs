using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace scms.Infrastructure.Data;

public class ScmsDbContext(DbContextOptions<ScmsDbContext> options) : DbContext(options)
{
    public DbSet<Module> Modules { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<ModulePermission> ModulePermissions { get; set; }
    public DbSet<PlanModule> PlanModules { get; set; }
    public DbSet<Tenant> Tenants { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Module>(ConfigModule);
        modelBuilder.Entity<Permission>(ConfigPermission);
        modelBuilder.Entity<Plan>(ConfigPlan);
        modelBuilder.Entity<ModulePermission>(ConfigModulePermission);
        modelBuilder.Entity<PlanModule>(ConfigPlanModule);
        modelBuilder.Entity<Tenant>(ConfigTenant);
    }

    private static void ConfigModule(EntityTypeBuilder<Module> e)
    {
    }

    private static void ConfigPermission(EntityTypeBuilder<Permission> e)
    {
        e.HasData(DataSeed.Permissions);
    }

    private static void ConfigPlan(EntityTypeBuilder<Plan> e)
    {
    }

    private static void ConfigModulePermission(EntityTypeBuilder<ModulePermission> e)
    {
    }

    private static void ConfigPlanModule(EntityTypeBuilder<PlanModule> e)
    {
    }

    private static void ConfigTenant(EntityTypeBuilder<Tenant> e)
    {
        e.ToTable(tb =>
        {
            tb.HasCheckConstraint(
                "ck_tenant_code_alnum_hyphen",
                "tenant_code ~ '^(?!-)(?!.*--)[A-Za-z0-9-]+(?<!-)$'");
        });
    }
}
