using SCMS.Shared.Entities;

namespace scms.Domain.Entities.Tenant;

public class Patient : AuditableEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
}