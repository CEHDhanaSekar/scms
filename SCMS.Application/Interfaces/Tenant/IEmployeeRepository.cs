using scms.Domain.Entities.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Employee>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Employee employee, CancellationToken ct = default);
    Task UpdateAsync(Employee employee, CancellationToken ct = default);
    Task DeleteAsync(Employee employee, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
