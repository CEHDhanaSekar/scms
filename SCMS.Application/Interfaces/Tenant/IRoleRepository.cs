using scms.Domain.Entities.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface IRoleRepository
{
    Task<Role?> GetByIdAsync(Guid id, bool onlyActive = false, CancellationToken ct = default);
    Task<List<Role>> GetAllAsync(bool onlyActive = false, CancellationToken ct = default);
    Task AddAsync(Role role, CancellationToken ct = default);
    Task UpdateAsync(Role role, CancellationToken ct = default);
    Task DeleteAsync(Role role, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
