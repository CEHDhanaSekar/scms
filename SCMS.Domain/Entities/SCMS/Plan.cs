using SCMS.Shared.Entities;

public class Plan : AuditableEntity
{
    public string PlanName { get; set; } = string.Empty;
    public int MaxUsers { get; set; }
    public int MaxEmployees { get; set; }
    public decimal PriceMonthly { get; set; }
    public decimal PriceYearly { get; set; }
    public BillingCycle BillingCycle { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
    public ICollection<PlanModule> PlanModules { get; set; } = new List<PlanModule>();
}

public enum BillingCycle
{
    Monthly = 1,
    Yearly = 2
}