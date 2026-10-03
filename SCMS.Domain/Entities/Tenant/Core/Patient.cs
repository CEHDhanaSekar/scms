using scms.Domain.Entities.Tenant.Common;
using SCMS.Shared.Entities;

namespace scms.Domain.Entities.Tenant.Core;

public class Patient : AuditableEntity
{
    public string PatientCode { get; set; } = string.Empty;   // auto-generated, e.g. PAT-0001

    public Guid SalutationId { get; set; }
    public MasterValues Salutation { get; set; } = null!;

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }

    public Guid GenderId { get; set; }
    public MasterValues Gender { get; set; } = null!;

    public Guid? BloodGroupId { get; set; }
    public MasterValues? BloodGroup { get; set; }

    public string ContactNumber { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Address { get; set; } = string.Empty;

    public string? EmergencyContactName { get; set; }
    public Guid? EmergencyContactRelationId { get; set; }
    public MasterValues? EmergencyContactRelation { get; set; }
    public string? EmergencyContactNumber { get; set; }

    public bool IsActive { get; set; } = true;   // deceased/blocked flag — distinct from soft-delete
}