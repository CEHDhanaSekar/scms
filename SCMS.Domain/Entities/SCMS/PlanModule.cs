using SCMS.Shared.Entities;

public class PlanModule : BaseEntity
{
    public Guid PlanId { get; set; }
    public Plan Plan { get; set; } = null!;

    public Guid ModuleId { get; set; }
    public Module Module { get; set; } = null!;

    public bool IsEnabled { get; set; } = true;
}