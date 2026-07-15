using SCMS.Domain.Enums;
using SCMS.Shared.Entities;

namespace scms.Domain.Entities.Tenant;

public class Employee : AuditableEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public EmployeeType Type { get; set; }          // Doctor, Nurse, Receptionist, Admin, etc.

    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = default!;

    public Guid? SpecializationId { get; set; }      // only meaningful for Doctors
    public Specialization? Specialization { get; set; }

    public User? User { get; set; }                  // 1:1 — nullable, not every employee logs in
}