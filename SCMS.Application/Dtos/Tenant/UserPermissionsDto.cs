namespace scms.Application.Dtos.Tenant;

public class UserPermissionDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;       // e.g. "patients.create"
    public string? Description { get; set; }
    public string RoleName { get; set; } = string.Empty;   // role that granted this permission
}

public class UserPermissionsResponseDto
{
    public Guid UserId { get; set; }
    public List<UserPermissionDto> Permissions { get; set; } = new();
}
