using scms.Domain.Entities.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface ISpecializationRepository
{
    Task<Specialization?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Specialization>> GetAllAsync(CancellationToken ct = default);
    Task AddAsync(Specialization specialization, CancellationToken ct = default);
    Task UpdateAsync(Specialization specialization, CancellationToken ct = default);
    Task DeleteAsync(Specialization specialization, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
