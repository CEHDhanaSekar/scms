namespace scms.Application.DTOs;

public class TenantDto
{
    public Guid Id { get; set; }
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
    public bool IsActive { get; set; }
    public scms.Domain.Entities.SCMS.TenantStatus Status { get; set; }
    public Guid PlanId { get; set; }
}

public class CreateTenantDto
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
}

public class UpdateTenantDto
{
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
    public bool IsActive { get; set; }
    public scms.Domain.Entities.SCMS.TenantStatus Status { get; set; }
    public Guid PlanId { get; set; }
}
