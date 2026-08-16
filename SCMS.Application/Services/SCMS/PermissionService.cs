using AutoMapper;
using scms.Application.Dtos.SCMS;
using scms.Application.Interfaces;
using SCMS.Shared.Exceptions;
using scms.Domain.Entities.SCMS;

namespace scms.Application.Services.SCMS;

public interface IPermissionService
{
    Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync();
    Task<PermissionDto?> GetPermissionByIdAsync(Guid id);
    Task<PermissionDto> CreatePermissionAsync(CreatePermissionDto dto);
    Task<bool> UpdatePermissionAsync(Guid id, UpdatePermissionDto dto);
    Task<bool> DeletePermissionAsync(Guid id);
}

public class PermissionService(IPermissionRepository permissionRepository, IMapper mapper) : IPermissionService
{
    public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
    {
        var permissions = await permissionRepository.GetAllAsync();
        return mapper.Map<IEnumerable<PermissionDto>>(permissions);
    }

    public async Task<PermissionDto?> GetPermissionByIdAsync(Guid id)
    {
        var permission = await permissionRepository.GetByIdAsync(id);
        if (permission == null) throw new NotFoundException("Permission not found");
        return mapper.Map<PermissionDto>(permission);
    }

    public async Task<PermissionDto> CreatePermissionAsync(CreatePermissionDto dto)
    {
        var permission = mapper.Map<Permission>(dto);
        permission.PermissionKey = dto.PermissionName.Trim().ToUpper().Replace(" ", "_");
        var created = await permissionRepository.AddAsync(permission);
        return mapper.Map<PermissionDto>(created);
    }

    public async Task<bool> UpdatePermissionAsync(Guid id, UpdatePermissionDto dto)
    {
        var permission = await permissionRepository.GetByIdAsync(id);
        if (permission == null) return false;

        mapper.Map(dto, permission);

        await permissionRepository.UpdateAsync(permission);
        return true;
    }

    public async Task<bool> DeletePermissionAsync(Guid id)
    {
        var permission = await permissionRepository.GetByIdAsync(id);
        if (permission == null) return false;

        await permissionRepository.DeleteAsync(id);
        return true;
    }
}
