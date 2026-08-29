using scms.Domain.Entities.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface ITenantPermissionRepository
{
    Task<TenantPermission?> GetByIdAsync(Guid id, bool onlyActive = false, CancellationToken ct = default);
    Task<List<TenantPermission>> GetAllAsync(bool onlyActive = false, CancellationToken ct = default);
}
