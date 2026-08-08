using scms.Application.Dtos.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface IPermissionService
{
    Task<PermissionDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<PermissionDto>> GetAllAsync(CancellationToken ct = default);
}
