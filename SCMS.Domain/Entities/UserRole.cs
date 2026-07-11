// SCMS.Domain/Entities/UserRole.cs
using SCMS.Shared.Entities;

namespace SCMS.Domain.Entities;

public class UserRole : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public Guid RoleId { get; set; }
    public Role Role { get; set; } = default!;
}