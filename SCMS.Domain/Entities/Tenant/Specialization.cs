using SCMS.Shared.Entities;

namespace scms.Domain.Entities.Tenant;

public class Specialization : AuditableEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public Guid? DepartmentId { get; set; }
    public Department? Department { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}