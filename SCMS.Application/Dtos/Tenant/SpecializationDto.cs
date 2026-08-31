namespace scms.Application.Dtos.Tenant;

public class SpecializationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? DepartmentId { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateSpecializationDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? DepartmentId { get; set; }
}

public class UpdateSpecializationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid? DepartmentId { get; set; }
}
