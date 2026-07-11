// SCMS.Domain/Entities/RolePermission.cs
using SCMS.Shared.Entities;

namespace SCMS.Domain.Entities;

public class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }
    public Role Role { get; set; } = default!;

    public Guid PermissionId { get; set; }
    public Permission Permission { get; set; } = default!;
}