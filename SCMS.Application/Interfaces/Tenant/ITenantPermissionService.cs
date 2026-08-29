using scms.Application.Dtos.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface ITenantPermissionService
{
    Task<TenantPermissionDto?> GetByIdAsync(Guid id, bool onlyActive = false, CancellationToken ct = default);
    Task<List<TenantPermissionDto>> GetAllAsync(bool onlyActive = false, CancellationToken ct = default);
}
