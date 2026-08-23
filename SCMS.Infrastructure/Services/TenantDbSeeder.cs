using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces;
using scms.Infrastructure.Persistence;
using scms.Domain.Entities.Tenant;

namespace scms.Infrastructure.Services;

public class TenantDbSeeder : ITenantDbSeeder
{
    private readonly IPasswordHasherService _passwordHasherService;

    public TenantDbSeeder(IPasswordHasherService passwordHasherService)
    {
        _passwordHasherService = passwordHasherService;
    }

    public async Task<(string Username, string RawPassword)> SeedTenantDataAsync(
        string connectionString,
        string tenantCode,
        string email,
        IEnumerable<string> permissionKeys,
        string planName,
        CancellationToken ct = default)
    {
        var assemblyName = typeof(DependencyInjection).Assembly.GetName().Name!;
        var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
        optionsBuilder.UseNpgsql(connectionString, b =>
        {
            b.MigrationsAssembly(assemblyName);
            b.MigrationsHistoryTable("__ef_migrations_history_tenant");
            b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
        }).UseSnakeCaseNamingConvention();

        await using var tenantCtx = new TenantDbContext(optionsBuilder.Options);

        // Seed permissions
        var tenantPermissions = permissionKeys
            .Where(code => !string.IsNullOrWhiteSpace(code))
            .Select(code => new TenantPermission
            {
                Code = code!,
                Description = $"Auto-seeded from plan {planName}"
            })
            .ToList();

        if (tenantPermissions.Count > 0)
        {
            tenantCtx.Permissions.AddRange(tenantPermissions);
        }
        await tenantCtx.SaveChangesAsync(ct);

        // Create Admin role
        var adminRole = new Domain.Entities.Tenant.Role
        {
            Name = "Admin",
            Description = "Default System Administrator"
        };
        tenantCtx.Roles.Add(adminRole);
        await tenantCtx.SaveChangesAsync(ct);

        // Link RolePermissions
        var rolePermissions = tenantPermissions.Select(p => new scms.Domain.Entities.Tenant.RolePermission
        {
            RoleId = adminRole.Id,
            PermissionId = p.Id
        }).ToList();
        tenantCtx.RolePermissions.AddRange(rolePermissions);
        await tenantCtx.SaveChangesAsync(ct);

        // Create Admin user
        string rawPassword = Guid.NewGuid().ToString("N")[..12]; // simple generator
        var adminUser = new Domain.Entities.Tenant.User
        {
            Username = $"admin@{tenantCode}",
            Email = email,
            PasswordHash = _passwordHasherService.HashPassword(rawPassword),
            MustChangePassword = true,
            IsActive = true
        };
        tenantCtx.Users.Add(adminUser);
        await tenantCtx.SaveChangesAsync(ct);

        // Link UserRole
        tenantCtx.UserRoles.Add(new Domain.Entities.Tenant.UserRole
        {
            UserId = adminUser.Id,
            RoleId = adminRole.Id
        });
        await tenantCtx.SaveChangesAsync(ct);

        return (adminUser.Username, rawPassword);
    }

    public async Task UpdateTenantPermissionsAsync(
        string connectionString,
        IEnumerable<string> permissionKeys,
        string planName,
        CancellationToken ct = default)
    {
        var assemblyName = typeof(DependencyInjection).Assembly.GetName().Name!;
        var optionsBuilder = new DbContextOptionsBuilder<TenantDbContext>();
        optionsBuilder.UseNpgsql(connectionString, b =>
        {
            b.MigrationsAssembly(assemblyName);
            b.MigrationsHistoryTable("__ef_migrations_history_tenant");
            b.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
        }).UseSnakeCaseNamingConvention();

        await using var tenantCtx = new TenantDbContext(optionsBuilder.Options);

        var existingPermissions = await tenantCtx.Permissions.ToListAsync(ct);
        var existingPermissionCodes = existingPermissions.Select(p => p.Code).ToHashSet();
        
        var newPermissionKeys = permissionKeys.Where(code => !string.IsNullOrWhiteSpace(code)).ToHashSet();

        // Find permissions to add
        var permissionsToAdd = newPermissionKeys
            .Where(code => !existingPermissionCodes.Contains(code))
            .Select(code => new TenantPermission
            {
                Code = code,
                Description = $"Auto-seeded from plan {planName}"
            })
            .ToList();

        // Find permissions to remove
        var permissionsToRemove = existingPermissions
            .Where(p => !newPermissionKeys.Contains(p.Code))
            .ToList();

        if (permissionsToAdd.Count > 0)
        {
            tenantCtx.Permissions.AddRange(permissionsToAdd);
        }

        if (permissionsToRemove.Count > 0)
        {
            tenantCtx.Permissions.RemoveRange(permissionsToRemove);
        }

        await tenantCtx.SaveChangesAsync(ct);
        
        // Add new permissions to Admin role
        if (permissionsToAdd.Count > 0)
        {
            var adminRole = await tenantCtx.Roles.FirstOrDefaultAsync(r => r.Name == "Admin", ct);
            if (adminRole != null)
            {
                var rolePermissions = permissionsToAdd.Select(p => new scms.Domain.Entities.Tenant.RolePermission
                {
                    RoleId = adminRole.Id,
                    PermissionId = p.Id
                }).ToList();
                tenantCtx.RolePermissions.AddRange(rolePermissions);
                await tenantCtx.SaveChangesAsync(ct);
            }
        }
    }
}
