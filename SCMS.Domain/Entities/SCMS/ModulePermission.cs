// ModulePermission.cs — which permissions are valid for a module
// PermissionKey here is a precomputed composite (e.g. "PATIENT_READ") —
// this is the value you'll actually put in JWT claims / frontend guards,
// since "READ" alone isn't specific enough to authorize anything.
using SCMS.Shared.Entities;

public class ModulePermission : BaseEntity
{
    public Guid ModuleId { get; set; }
    public Module Module { get; set; } = null!;

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; } = null!;

    public string PermissionKey { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}