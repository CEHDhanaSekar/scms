using SCMS.Shared.Entities;

namespace scms.Domain.Entities.SCMS;

public class OwnerUser : AuditableEntity
{
    public string Name { get; set; } = default!;
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Mobile { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public bool IsActive { get; set; } = true;
    public DateTime? LastLoginAt { get; set; }

    public ICollection<OwnerRefreshToken> RefreshTokens { get; set; } = new List<OwnerRefreshToken>();
}
