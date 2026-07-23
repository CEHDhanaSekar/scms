namespace scms.Application.DTOs;

public class ModulePermissionDto
{
    public Guid Id { get; set; }
    public Guid ModuleId { get; set; }
    public Guid PermissionId { get; set; }
    public string PermissionKey { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

public class CreateModulePermissionDto
{
    public Guid ModuleId { get; set; }
    public Guid PermissionId { get; set; }
    public string PermissionKey { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public class UpdateModulePermissionDto
{
    public string PermissionKey { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
