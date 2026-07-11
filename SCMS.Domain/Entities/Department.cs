using SCMS.Shared.Entities;

namespace SCMS.Domain.Entities;

public class Department : AuditableEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public ICollection<Specialization> Specializations { get; set; } = new List<Specialization>();
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}