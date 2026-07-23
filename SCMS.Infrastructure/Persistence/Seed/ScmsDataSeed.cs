// SCMS.Infrastructure/Persistence/Seed/DataSeed.cs
public static class DataSeed
{
    // Fixed GUIDs — never change these once migrated, or EF will treat it as delete+insert
    public static readonly Guid ReadPermissionId   = Guid.Parse("A1111111-0000-0000-0000-000000000001");
    public static readonly Guid WritePermissionId  = Guid.Parse("A1111111-0000-0000-0000-000000000002");
    public static readonly Guid DeletePermissionId = Guid.Parse("A1111111-0000-0000-0000-000000000003");
    public static readonly Guid ExportPermissionId = Guid.Parse("A1111111-0000-0000-0000-000000000004");

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