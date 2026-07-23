namespace scms.Application.DTOs;

public class PlanModuleDto
{
    public Guid Id { get; set; }
    public Guid PlanId { get; set; }
    public Guid ModuleId { get; set; }
    public bool IsEnabled { get; set; }
}

public class CreatePlanModuleDto
{
    public Guid PlanId { get; set; }
    public Guid ModuleId { get; set; }
    public bool IsEnabled { get; set; } = true;
}

public class UpdatePlanModuleDto
{
    public bool IsEnabled { get; set; }
}
