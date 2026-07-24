using AutoMapper;
using scms.Application.DTOs;
using scms.Application.Interfaces;
using SCMS.Shared.Exceptions;

namespace scms.Application.Services;

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
