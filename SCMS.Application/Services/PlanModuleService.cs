using AutoMapper;
using scms.Application.DTOs;
using scms.Application.Interfaces;
using SCMS.Shared.Exceptions;

namespace scms.Application.Services;

public interface IPlanModuleService
{
    Task<IEnumerable<PlanModuleDto>> GetAllPlanModulesAsync();
    Task<PlanModuleDto?> GetPlanModuleByIdAsync(Guid id);
    Task<PlanModuleDto> CreatePlanModuleAsync(CreatePlanModuleDto dto);
    Task<bool> UpdatePlanModuleAsync(Guid id, UpdatePlanModuleDto dto);
    Task<bool> DeletePlanModuleAsync(Guid id);
}

public class PlanModuleService(IPlanModuleRepository repository, IMapper mapper) : IPlanModuleService
{
    public async Task<IEnumerable<PlanModuleDto>> GetAllPlanModulesAsync()
    {
        var entities = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<PlanModuleDto>>(entities);
    }

    public async Task<PlanModuleDto?> GetPlanModuleByIdAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) throw new NotFoundException("PlanModule not found");
        return mapper.Map<PlanModuleDto>(entity);
    }

    public async Task<PlanModuleDto> CreatePlanModuleAsync(CreatePlanModuleDto dto)
    {
        var entity = mapper.Map<PlanModule>(dto);
        var created = await repository.AddAsync(entity);
        return mapper.Map<PlanModuleDto>(created);
    }

    public async Task<bool> UpdatePlanModuleAsync(Guid id, UpdatePlanModuleDto dto)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;

        mapper.Map(dto, entity);

        await repository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeletePlanModuleAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;

        await repository.DeleteAsync(id);
        return true;
    }
}
