using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using scms.Domain.Entities.SCMS;

namespace scms.Infrastructure.Data;

public class ScmsDbContext(DbContextOptions<ScmsDbContext> options) : DbContext(options)
{
    public DbSet<Module> Modules { get; set; }
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<Plan> Plans { get; set; }
    public DbSet<ModulePermission> ModulePermissions { get; set; }
    public DbSet<PlanModule> PlanModules { get; set; }
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<OwnerUser> OwnerUsers { get; set; }
    public DbSet<OwnerRefreshToken> OwnerRefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Module>(ConfigModule);
        modelBuilder.Entity<Permission>(ConfigPermission);
        modelBuilder.Entity<Plan>(ConfigPlan);
        modelBuilder.Entity<ModulePermission>(ConfigModulePermission);
        modelBuilder.Entity<PlanModule>(ConfigPlanModule);
        modelBuilder.Entity<Tenant>(ConfigTenant);
        modelBuilder.Entity<OwnerUser>(ConfigOwnerUser);
        modelBuilder.Entity<OwnerRefreshToken>(ConfigOwnerRefreshToken);
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

    private static void ConfigOwnerUser(EntityTypeBuilder<OwnerUser> e)
    {
        e.HasIndex(u => u.Username).IsUnique();
        e.HasIndex(u => u.Email).IsUnique();
        e.HasData(DataSeed.DefaultOwnerAdmin);
    }

    private static void ConfigOwnerRefreshToken(EntityTypeBuilder<OwnerRefreshToken> e)
    {
        e.HasIndex(r => r.Token).IsUnique();
        e.HasOne(r => r.OwnerUser)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(r => r.OwnerUserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
