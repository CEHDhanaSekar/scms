using scms.Domain.Entities.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface ITenantPermissionRepository
{
    Task<TenantPermission?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<TenantPermission>> GetAllAsync(CancellationToken ct = default);
}
