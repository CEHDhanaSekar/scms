// SCMS.Domain/Entities/Role.cs
using SCMS.Shared.Entities;

namespace SCMS.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; set; } = default!;      // Admin, Doctor, Receptionist, etc.
    public string? Description { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}