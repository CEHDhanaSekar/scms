// SCMS.Infrastructure/Persistence/Seed/DataSeed.cs
public static class DataSeed
{
    // Fixed GUIDs — never change these once migrated, or EF will treat it as delete+insert
    public static readonly Guid ReadPermissionId   = Guid.Parse("A1111111-0000-0000-0000-000000000001");
    public static readonly Guid WritePermissionId  = Guid.Parse("A1111111-0000-0000-0000-000000000002");
    public static readonly Guid DeletePermissionId = Guid.Parse("A1111111-0000-0000-0000-000000000003");
    public static readonly Guid ExportPermissionId = Guid.Parse("A1111111-0000-0000-0000-000000000004");

    public static readonly Guid OwnerAdminId = Guid.Parse("B2222222-0000-0000-0000-000000000001");

    // ── Master plan + vktech owner tenant ─────────────────────────────────────
    public static readonly Guid MasterPlanId   = Guid.Parse("C3333333-0000-0000-0000-000000000001");
    public static readonly Guid MasterTenantId = Guid.Parse("D4444444-0000-0000-0000-000000000001");

    public static Plan MasterPlan => new()
    {
        Id = MasterPlanId,
        PlanName = "Master",
        MaxUsers = int.MaxValue,
        MaxEmployees = int.MaxValue,
        PriceMonthly = 0,
        PriceYearly = 0,
        BillingCycle = BillingCycle.Yearly,
        IsActive = true,
        CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        CreatedBy = "System"
    };

    public static Tenant VkTechTenant => new()
    {
        Id = MasterTenantId,
        TenantCode = "vktech",
        Name = "VK Tech (Owner)",
        ContactPersonName = "Admin",
        Email = "admin@vktech.com",
        MobilePhone = "0000000000",
        DomainUrl = "http://localhost:4200",
        IsActive = true,
        PlanId = MasterPlanId,
        CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        CreatedBy = "System"
    };

    private static string DefaultAdminPasswordHash => GenerateDefaultAdminPasswordHash();

    private static string GenerateDefaultAdminPasswordHash()
    {
        byte[] fixedSalt = new byte[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160 };
        byte[] hash = System.Security.Cryptography.Rfc2898DeriveBytes.Pbkdf2(
            "Admin@123",
            fixedSalt,
            100_000,
            System.Security.Cryptography.HashAlgorithmName.SHA512,
            32);
        return $"{Convert.ToBase64String(fixedSalt)}.{Convert.ToBase64String(hash)}";
    }

    public static scms.Domain.Entities.SCMS.OwnerUser DefaultOwnerAdmin => new scms.Domain.Entities.SCMS.OwnerUser
    {
        Id = OwnerAdminId,
        Name = "Super Admin",
        Username = "admin",
        Email = "admin@scms.com",
        Mobile = "9876543210",
        PasswordHash = DefaultAdminPasswordHash,
        IsActive = true,
        CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
        CreatedBy = "System"
    };

    public static Permission[] Permissions => new[]
    {
        new Permission
        {
            Id = ReadPermissionId,
            PermissionKey = "READ",
            PermissionName = "Read",
            Description = "View/read access to a module's data",
            IsActive = true,
        },
        new Permission
        {
            Id = WritePermissionId,
            PermissionKey = "WRITE",
            PermissionName = "Write",
            Description = "Create or update a module's data",
            IsActive = true,
        },
        new Permission
        {
            Id = DeletePermissionId,
            PermissionKey = "DELETE",
            PermissionName = "Delete",
            Description = "Delete/remove a module's data",
            IsActive = true,
        },
        new Permission
        {
            Id = ExportPermissionId,
            PermissionKey = "EXPORT",
            PermissionName = "Export",
            Description = "Export a module's data (reports/CSV)",
            IsActive = true,
        },
    };
}