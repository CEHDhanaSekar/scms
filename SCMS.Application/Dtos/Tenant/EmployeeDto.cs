using SCMS.Domain.Enums;

namespace scms.Application.Dtos.Tenant;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public EmployeeType Type { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid? SpecializationId { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateEmployeeDto
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public EmployeeType Type { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid? SpecializationId { get; set; }
}

public class UpdateEmployeeDto
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public EmployeeType Type { get; set; }
    public Guid DepartmentId { get; set; }
    public Guid? SpecializationId { get; set; }
}
