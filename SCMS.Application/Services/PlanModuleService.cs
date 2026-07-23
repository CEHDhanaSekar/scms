using scms.Application.DTOs;
using scms.Application.Interfaces;

namespace scms.Application.Services;

public interface IPlanModuleService
{
    Task<IEnumerable<PlanModuleDto>> GetAllPlanModulesAsync();
    Task<PlanModuleDto?> GetPlanModuleByIdAsync(Guid id);
    Task<PlanModuleDto> CreatePlanModuleAsync(CreatePlanModuleDto dto);
    Task<bool> UpdatePlanModuleAsync(Guid id, UpdatePlanModuleDto dto);
    Task<bool> DeletePlanModuleAsync(Guid id);
}

public class PlanModuleService(IPlanModuleRepository repository) : IPlanModuleService
{
    public async Task<IEnumerable<PlanModuleDto>> GetAllPlanModulesAsync()
    {
        var entities = await repository.GetAllAsync();
        return entities.Select(MapToDto);
    }

    public async Task<PlanModuleDto?> GetPlanModuleByIdAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return null;
        return MapToDto(entity);
    }

    public async Task<PlanModuleDto> CreatePlanModuleAsync(CreatePlanModuleDto dto)
    {
        var entity = new PlanModule
        {
            PlanId = dto.PlanId,
            ModuleId = dto.ModuleId,
            IsEnabled = dto.IsEnabled
        };

        var created = await repository.AddAsync(entity);
        return MapToDto(created);
    }

    public async Task<bool> UpdatePlanModuleAsync(Guid id, UpdatePlanModuleDto dto)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;

        entity.IsEnabled = dto.IsEnabled;

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

    private static PlanModuleDto MapToDto(PlanModule entity)
    {
        return new PlanModuleDto
        {
            Id = entity.Id,
            PlanId = entity.PlanId,
            ModuleId = entity.ModuleId,
            IsEnabled = entity.IsEnabled
        };
    }
}
