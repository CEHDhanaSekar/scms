namespace scms.Application.Dtos.Tenant;

public class DepartmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public bool IsDeleted { get; set; }
}

public class CreateDepartmentDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}

public class UpdateDepartmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
