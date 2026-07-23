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

public class PlanService(IPlanRepository repository) : IPlanService
{
    public async Task<IEnumerable<PlanDto>> GetAllPlansAsync()
    {
        var entities = await repository.GetAllAsync();
        return entities.Select(MapToDto);
    }

    public async Task<PlanDto?> GetPlanByIdAsync(Guid id)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) throw new NotFoundException("Plan not found");
        return MapToDto(entity);
    }

    public async Task<PlanDto> CreatePlanAsync(CreatePlanDto dto)
    {
        var entity = new Plan
        {
            PlanName = dto.PlanName,
            MaxUsers = dto.MaxUsers,
            MaxEmployees = dto.MaxEmployees,
            PriceMonthly = dto.PriceMonthly,
            PriceYearly = dto.PriceYearly,
            BillingCycle = dto.BillingCycle,
            IsActive = dto.IsActive
        };

        var created = await repository.AddAsync(entity);
        return MapToDto(created);
    }

    public async Task<bool> UpdatePlanAsync(Guid id, UpdatePlanDto dto)
    {
        var entity = await repository.GetByIdAsync(id);
        if (entity == null) return false;

        entity.PlanName = dto.PlanName;
        entity.MaxUsers = dto.MaxUsers;
        entity.MaxEmployees = dto.MaxEmployees;
        entity.PriceMonthly = dto.PriceMonthly;
        entity.PriceYearly = dto.PriceYearly;
        entity.BillingCycle = dto.BillingCycle;
        entity.IsActive = dto.IsActive;

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

    private static PlanDto MapToDto(Plan entity)
    {
        return new PlanDto
        {
            Id = entity.Id,
            PlanName = entity.PlanName,
            MaxUsers = entity.MaxUsers,
            MaxEmployees = entity.MaxEmployees,
            PriceMonthly = entity.PriceMonthly,
            PriceYearly = entity.PriceYearly,
            BillingCycle = entity.BillingCycle,
            IsActive = entity.IsActive
        };
    }
}
