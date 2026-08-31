namespace scms.Application.Dtos.Tenant;

public class TenantPermissionDto
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
