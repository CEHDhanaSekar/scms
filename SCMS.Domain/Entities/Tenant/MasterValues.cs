using SCMS.Shared.Entities;

namespace scms.Domain.Entities.Tenant;

public class MasterValues : BaseEntity
{
    public bool IsActive { get; set; } = true;
    public string DisplayName { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Key { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
}