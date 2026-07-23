namespace scms.Application.Interfaces;

public interface IPlanRepository
{
    Task<IEnumerable<Plan>> GetAllAsync();
    Task<Plan?> GetByIdAsync(Guid id);
    Task<Plan> AddAsync(Plan plan);
    Task UpdateAsync(Plan plan);
    Task DeleteAsync(Guid id);
}
