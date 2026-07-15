using SCMS.Shared.Entities;

namespace scms.Domain.Entities.Tenant;

public class Department : AuditableEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}