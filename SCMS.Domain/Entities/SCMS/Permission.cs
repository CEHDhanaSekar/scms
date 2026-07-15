using SCMS.Shared.Entities;

public class Permission : BaseEntity
{
    public string PermissionKey { get; set; } = string.Empty;  // e.g. "READ", "WRITE"
    public string PermissionName { get; set; } = string.Empty; // e.g. "Read", "Write"
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<ModulePermission> ModulePermissions { get; set; } = new List<ModulePermission>();
}