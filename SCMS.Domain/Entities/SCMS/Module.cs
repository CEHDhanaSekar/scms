using SCMS.Shared.Entities;

public class Module : BaseEntity
{
    public string ModuleKey { get; set; } = string.Empty;   // e.g. "PATIENT", "BILLING"
    public string ModuleName { get; set; } = string.Empty;  // e.g. "Patient Management"
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<PlanModule> PlanModules { get; set; } = new List<PlanModule>();
    public ICollection<ModulePermission> ModulePermissions { get; set; } = new List<ModulePermission>();
}