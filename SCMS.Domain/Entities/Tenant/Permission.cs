// SCMS.Domain/Entities/Permission.cs
using SCMS.Shared.Entities;

namespace scms.Domain.Entities.Tenant;

public class Permission : BaseEntity
{
    public string Code { get; set; } = default!;      // e.g. "patients.create", "billing.view"
    public string? Description { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}