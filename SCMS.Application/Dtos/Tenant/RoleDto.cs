namespace scms.Application.Dtos.Tenant;

public class RoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    
    // We can also return permissions
    public List<Guid> PermissionIds { get; set; } = new();
}

public class CreateRoleDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public List<Guid> PermissionIds { get; set; } = new();
}

public class UpdateRoleDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public List<Guid> PermissionIds { get; set; } = new();
}
