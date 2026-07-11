// SCMS.Domain/Entities/User.cs
using SCMS.Shared.Entities;

namespace SCMS.Domain.Entities;

public class User : AuditableEntity
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;

    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }

    public Guid? EmployeeId { get; set; }             // nullable — supports non-employee accounts (pure system admin)
    public Employee? Employee { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}