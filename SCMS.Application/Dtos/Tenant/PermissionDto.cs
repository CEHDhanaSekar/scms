namespace scms.Application.Dtos.Tenant;

public class PermissionDto
{
    public Guid Id { get; set; }
    public string PermissionKey { get; set; } = string.Empty;
    public string PermissionName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
