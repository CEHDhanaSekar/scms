using AutoMapper;
using scms.Application.DTOs;
using scms.Application.Interfaces;
using SCMS.Shared.Exceptions;

namespace scms.Application.Services;

public interface IModulePermissionService
{
    Task<IEnumerable<ModulePermissionDto>> GetAllModulePermissionsAsync();
    Task<ModulePermissionDto?> GetModulePermissionByIdAsync(Guid id);
    Task<ModulePermissionDto> CreateModulePermissionAsync(CreateModulePermissionDto dto);
    Task<bool> UpdateModulePermissionAsync(Guid id, UpdateModulePermissionDto dto);
    Task<bool> DeleteModulePermissionAsync(Guid id);
}

public class ModulePermissionService(IModulePermissionRepository repository, IMapper mapper) : IModulePermissionService
{
    public async Task<IEnumerable<ModulePermissionDto>> GetAllModulePermissionsAsync()
    {
        var entities = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<ModulePermissionDto>>(entities);
    }

    public async Task<ModulePermissionDto?> GetModulePermissionByIdAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) throw new NotFoundException("ModulePermission not found");
        return mapper.Map<ModulePermissionDto>(entity);
    }

    public async Task<ModulePermissionDto> CreateModulePermissionAsync(CreateModulePermissionDto dto)
    {
        var entity = mapper.Map<ModulePermission>(dto);
        var created = await repository.AddAsync(entity);
        return mapper.Map<ModulePermissionDto>(created);
    }

    public async Task<bool> UpdateModulePermissionAsync(Guid id, UpdateModulePermissionDto dto)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;

        mapper.Map(dto, entity);

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
}
