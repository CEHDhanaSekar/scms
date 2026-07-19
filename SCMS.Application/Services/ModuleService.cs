using scms.Application.DTOs;
using scms.Application.Interfaces;

namespace scms.Application.Services;

public interface IModuleService
{
    Task<IEnumerable<ModuleDto>> GetAllModulesAsync();
    Task<ModuleDto?> GetModuleByIdAsync(Guid id);
    Task<ModuleDto> CreateModuleAsync(CreateModuleDto dto);
    Task<bool> UpdateModuleAsync(Guid id, UpdateModuleDto dto);
    Task<bool> DeleteModuleAsync(Guid id);
}

public class ModuleService(IModuleRepository moduleRepository) : IModuleService
{
    public async Task<IEnumerable<ModuleDto>> GetAllModulesAsync()
    {
        var modules = await moduleRepository.GetAllAsync();
        return modules.Select(MapToDto);
    }

    public async Task<ModuleDto?> GetModuleByIdAsync(Guid id)
    {
        var module = await moduleRepository.GetByIdAsync(id);
        if (module == null) return null;
        return MapToDto(module);
    }

    public async Task<ModuleDto> CreateModuleAsync(CreateModuleDto dto)
    {
        var module = new Module
        {
            ModuleKey = dto.ModuleKey,
            ModuleName = dto.ModuleName,
            Description = dto.Description,
            IsActive = dto.IsActive
        };

        var created = await moduleRepository.AddAsync(module);
        return MapToDto(created);
    }

    public async Task<bool> UpdateModuleAsync(Guid id, UpdateModuleDto dto)
    {
        var module = await moduleRepository.GetByIdAsync(id);
        if (module == null) return false;

        module.ModuleName = dto.ModuleName;
        module.Description = dto.Description;
        module.IsActive = dto.IsActive;

        await moduleRepository.UpdateAsync(module);
        return true;
    }

    public async Task<bool> DeleteModuleAsync(Guid id)
    {
        var module = await moduleRepository.GetByIdAsync(id);
        if (module == null) return false;

        await moduleRepository.DeleteAsync(id);
        return true;
    }

    private static ModuleDto MapToDto(Module module)
    {
        return new ModuleDto
        {
            Id = module.Id,
            ModuleKey = module.ModuleKey,
            ModuleName = module.ModuleName,
            Description = module.Description,
            IsActive = module.IsActive
        };
    }
}
