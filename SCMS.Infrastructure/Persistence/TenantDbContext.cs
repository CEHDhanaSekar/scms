using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using scms.Domain.Entities.SCMS;
using scms.Domain.Entities.Tenant;

namespace scms.Infrastructure.Persistence;

public class TenantDbContext(DbContextOptions<TenantDbContext> options) : DbContext(options)
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<TenantPermission> Permissions { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Patient> Patients { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(ConfigDepartment);
        modelBuilder.Entity<Specialization>(ConfigSpecialization);
        modelBuilder.Entity<Employee>(ConfigEmployee);
        modelBuilder.Entity<TenantPermission>(ConfigPermission);
        modelBuilder.Entity<Role>(ConfigRole);
        modelBuilder.Entity<RolePermission>(ConfigRolePermission);
        modelBuilder.Entity<User>(ConfigUser);
        modelBuilder.Entity<UserRole>(ConfigUserRole);
        modelBuilder.Entity<RefreshToken>(ConfigRefreshToken);
        modelBuilder.Entity<Patient>(ConfigPatient);
    }

    private static void ConfigDepartment(EntityTypeBuilder<Department> e)
    {
    }

    private static void ConfigSpecialization(EntityTypeBuilder<Specialization> e)
    {
    }

    private static void ConfigEmployee(EntityTypeBuilder<Employee> e)
    {
    }

    private static void ConfigPermission(EntityTypeBuilder<TenantPermission> e)
    {
    }

    private static void ConfigRole(EntityTypeBuilder<Role> e)
    {
    }

    private static void ConfigRolePermission(EntityTypeBuilder<RolePermission> e)
    {
    }

    private static void ConfigUser(EntityTypeBuilder<User> e)
    {
    }

    private static void ConfigUserRole(EntityTypeBuilder<UserRole> e)
    {
    }

    private static void ConfigRefreshToken(EntityTypeBuilder<RefreshToken> e)
    {
    }

    private static void ConfigPatient(EntityTypeBuilder<Patient> e)
    {
    }
}
