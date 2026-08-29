using scms.Application.Dtos.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface IRoleService
{
    Task<RoleDto?> GetByIdAsync(Guid id, bool onlyActive = false, CancellationToken ct = default);
    Task<List<RoleDto>> GetAllAsync(bool onlyActive = false, CancellationToken ct = default);
    Task<RoleDto> CreateAsync(CreateRoleDto dto, CancellationToken ct = default);
    Task<RoleDto> UpdateAsync(UpdateRoleDto dto, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
}
