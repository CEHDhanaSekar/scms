namespace scms.Application.DTOs;

public class PlanDto
{
    public Guid Id { get; set; }
    public string PlanName { get; set; } = string.Empty;
    public int MaxUsers { get; set; }
    public int MaxEmployees { get; set; }
    public decimal PriceMonthly { get; set; }
    public decimal PriceYearly { get; set; }
    public BillingCycle BillingCycle { get; set; }
    public bool IsActive { get; set; }
}

public class CreatePlanDto
{
    public string PlanName { get; set; } = string.Empty;
    public int MaxUsers { get; set; }
    public int MaxEmployees { get; set; }
    public decimal PriceMonthly { get; set; }
    public decimal PriceYearly { get; set; }
    public BillingCycle BillingCycle { get; set; }
    public bool IsActive { get; set; } = true;
}

public class UpdatePlanDto
{
    public string PlanName { get; set; } = string.Empty;
    public int MaxUsers { get; set; }
    public int MaxEmployees { get; set; }
    public decimal PriceMonthly { get; set; }
    public decimal PriceYearly { get; set; }
    public BillingCycle BillingCycle { get; set; }
    public bool IsActive { get; set; }
}
