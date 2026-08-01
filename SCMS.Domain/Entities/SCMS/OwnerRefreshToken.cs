using SCMS.Shared.Entities;

namespace scms.Domain.Entities.SCMS;

public class OwnerRefreshToken : BaseEntity
{
    public Guid OwnerUserId { get; set; }
    public OwnerUser OwnerUser { get; set; } = default!;

    public string Token { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? ReplacedByToken { get; set; }
    public string? CreatedByIp { get; set; }

    public bool IsActive => RevokedAt == null && DateTime.UtcNow < ExpiresAt;
}
