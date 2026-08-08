using scms.Domain.Entities.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface IDepartmentRepository
{
    Task<Department?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Department>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Department department, CancellationToken ct = default);
    Task UpdateAsync(Department department, CancellationToken ct = default);
    Task DeleteAsync(Department department, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
