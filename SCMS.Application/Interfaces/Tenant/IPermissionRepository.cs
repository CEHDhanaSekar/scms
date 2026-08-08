using scms.Domain.Entities.SCMS;

namespace scms.Application.Interfaces.Tenant;

public interface IPermissionRepository
{
    Task<Permission?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<Permission>> GetAllAsync(CancellationToken ct = default);
}
