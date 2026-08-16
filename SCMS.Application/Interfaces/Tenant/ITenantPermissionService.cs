using scms.Application.Dtos.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface ITenantPermissionService
{
    Task<TenantPermissionDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<TenantPermissionDto>> GetAllAsync(CancellationToken ct = default);
}
