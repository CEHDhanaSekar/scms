using scms.Application.Dtos.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(Guid id, bool onlyActive = false, CancellationToken ct = default);
    Task<List<UserDto>> GetAllAsync(bool onlyActive = false, CancellationToken ct = default);
    Task<UserDto> CreateAsync(CreateUserDto dto, string createdBy, CancellationToken ct = default);
    Task<UserDto> UpdateAsync(UpdateUserDto dto, string updatedBy, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, string deletedBy, CancellationToken ct = default);
    Task<UserPermissionsResponseDto?> GetPermissionsAsync(Guid userId, CancellationToken ct = default);
}
