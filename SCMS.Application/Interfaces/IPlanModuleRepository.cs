namespace scms.Application.Interfaces;

public interface IPlanModuleRepository
{
    Task<IEnumerable<PlanModule>> GetAllAsync();
    Task<IEnumerable<PlanModule>> GetByPlanIdAsync(Guid planId, CancellationToken ct = default);
    Task<PlanModule?> GetByIdAsync(Guid id);
    Task<PlanModule> AddAsync(PlanModule planModule);
    Task UpdateAsync(PlanModule planModule);
    Task DeleteAsync(Guid id);
}
