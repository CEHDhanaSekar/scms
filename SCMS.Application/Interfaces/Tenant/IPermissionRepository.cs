using TenantPermission = scms.Domain.Entities.Tenant.Permission;

namespace scms.Application.Interfaces.Tenant;

public interface IPermissionRepository
{
    Task<TenantPermission?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<TenantPermission>> GetAllAsync(CancellationToken ct = default);
}
