using SCMS.Shared.Entities;

public class Tenant : AuditableEntity
{
    public string TenantCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ContactPersonName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MobilePhone { get; set; } = string.Empty;
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }
    public string? PostalCode { get; set; }
    public string? DomainUrl { get; set; }
    public string? LogoUrl { get; set; }
    public bool IsActive { get; set; } = true;

    public Guid PlanId { get; set; }
    public Plan Plan { get; set; } = null!;
}