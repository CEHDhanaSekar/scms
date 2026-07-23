using scms.Application.DTOs;
using scms.Application.Interfaces;

namespace scms.Application.Services;

public interface IModulePermissionService
{
    Task<IEnumerable<ModulePermissionDto>> GetAllModulePermissionsAsync();
    Task<ModulePermissionDto?> GetModulePermissionByIdAsync(Guid id);
    Task<ModulePermissionDto> CreateModulePermissionAsync(CreateModulePermissionDto dto);
    Task<bool> UpdateModulePermissionAsync(Guid id, UpdateModulePermissionDto dto);
    Task<bool> DeleteModulePermissionAsync(Guid id);
}

public class ModulePermissionService(IModulePermissionRepository repository) : IModulePermissionService
{
    public async Task<IEnumerable<ModulePermissionDto>> GetAllModulePermissionsAsync()
    {
        var entities = await repository.GetAllAsync();
        return entities.Select(MapToDto);
    }

    public async Task<ModulePermissionDto?> GetModulePermissionByIdAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return null;
        return MapToDto(entity);
    }

    public async Task<ModulePermissionDto> CreateModulePermissionAsync(CreateModulePermissionDto dto)
    {
        var entity = new ModulePermission
        {
            ModuleId = dto.ModuleId,
            PermissionId = dto.PermissionId,
            PermissionKey = dto.PermissionKey,
            IsActive = dto.IsActive
        };

        var created = await repository.AddAsync(entity);
        return MapToDto(created);
    }

    public async Task<bool> UpdateModulePermissionAsync(Guid id, UpdateModulePermissionDto dto)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;

        entity.PermissionKey = dto.PermissionKey;
        entity.IsActive = dto.IsActive;

        await repository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeleteModulePermissionAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;

        await repository.DeleteAsync(id);
        return true;
    }

    private static ModulePermissionDto MapToDto(ModulePermission entity)
    {
        return new ModulePermissionDto
        {
            Id = entity.Id,
            ModuleId = entity.ModuleId,
            PermissionId = entity.PermissionId,
            PermissionKey = entity.PermissionKey,
            IsActive = entity.IsActive
        };
    }
}
