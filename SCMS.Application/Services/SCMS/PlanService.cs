using AutoMapper;
using scms.Application.Dtos.SCMS;
using scms.Application.Interfaces;
using SCMS.Shared.Exceptions;

namespace scms.Application.Services.SCMS;

public interface IPlanService
{
    Task<IEnumerable<PlanDto>> GetAllPlansAsync();
    Task<PlanDto?> GetPlanByIdAsync(Guid id);
    Task<PlanDto> CreatePlanAsync(CreatePlanDto dto);
    Task<bool> UpdatePlanAsync(Guid id, UpdatePlanDto dto);
    Task<bool> DeletePlanAsync(Guid id);
}

public class PlanService(IPlanRepository repository, IMapper mapper) : IPlanService
{
    public async Task<IEnumerable<PlanDto>> GetAllPlansAsync()
    {
        var entities = await repository.GetAllAsync();
        return mapper.Map<IEnumerable<PlanDto>>(entities);
    }

    public async Task<PlanDto?> GetPlanByIdAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) throw new NotFoundException("Plan not found");
        return mapper.Map<PlanDto>(entity);
    }

    public async Task<PlanDto> CreatePlanAsync(CreatePlanDto dto)
    {
        var entity = mapper.Map<Plan>(dto);
        var created = await repository.AddAsync(entity);
        return mapper.Map<PlanDto>(created);
    }

    public async Task<bool> UpdatePlanAsync(Guid id, UpdatePlanDto dto)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;

        mapper.Map(dto, entity);

        if (dto.PlanModules != null)
        {
            var modulesToRemove = entity.PlanModules
                .Where(pm => !dto.PlanModules.Any(dpm => dpm.ModuleId == pm.ModuleId))
                .ToList();

            foreach (var pm in modulesToRemove)
            {
                entity.PlanModules.Remove(pm);
            }

            foreach (var dpm in dto.PlanModules)
            {
                var existingPm = entity.PlanModules.FirstOrDefault(pm => pm.ModuleId == dpm.ModuleId);
                if (existingPm != null)
                {
                    existingPm.IsEnabled = dpm.IsEnabled;
                }
                else
                {
                    entity.PlanModules.Add(new PlanModule
                    {
                        Id = Guid.Empty, // Force EF Core to recognize this as a new entity (Added state) instead of existing (Modified state)
                        PlanId = id,
                        ModuleId = dpm.ModuleId,
                        IsEnabled = dpm.IsEnabled
                    });
                }
            }
        }

        await repository.UpdateAsync(entity);
        return true;
    }

    public async Task<bool> DeletePlanAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;

        await repository.DeleteAsync(id);
        return true;
    }
}
