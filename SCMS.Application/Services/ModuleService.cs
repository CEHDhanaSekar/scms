using AutoMapper;
using scms.Application.DTOs;
using scms.Application.Interfaces;
using SCMS.Shared.Exceptions;

namespace scms.Application.Services;

public interface IModuleService
{
    Task<IEnumerable<ModuleDto>> GetAllModulesAsync();
    Task<ModuleDto?> GetModuleByIdAsync(Guid id);
    Task<ModuleDto> CreateModuleAsync(CreateModuleDto dto);
    Task<bool> UpdateModuleAsync(Guid id, UpdateModuleDto dto);
    Task<bool> DeleteModuleAsync(Guid id);
}

public class ModuleService(IModuleRepository moduleRepository, IMapper mapper) : IModuleService
{
    public async Task<IEnumerable<ModuleDto>> GetAllModulesAsync()
    {
        var modules = await moduleRepository.GetAllAsync();
        return mapper.Map<IEnumerable<ModuleDto>>(modules);
    }

    public async Task<ModuleDto?> GetModuleByIdAsync(Guid id)
    {
        var module = await moduleRepository.GetByIdAsync(id);
        if (module == null) throw new NotFoundException("Module not found");
        return mapper.Map<ModuleDto>(module);
    }

    public async Task<ModuleDto> CreateModuleAsync(CreateModuleDto dto)
    {
        var module = mapper.Map<Module>(dto);
        var created = await moduleRepository.AddAsync(module);
        return mapper.Map<ModuleDto>(created);
    }

    public async Task<bool> UpdateModuleAsync(Guid id, UpdateModuleDto dto)
    {
        var module = await moduleRepository.GetByIdAsync(id);
        if (module == null) return false;

        mapper.Map(dto, module);

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
}
