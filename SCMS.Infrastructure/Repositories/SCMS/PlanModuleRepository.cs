using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces;
using scms.Infrastructure.Data;

namespace scms.Infrastructure.Repositories.SCMS;

public class PlanModuleRepository(ScmsDbContext context) : IPlanModuleRepository
{
    public async Task<IEnumerable<PlanModule>> GetAllAsync()
    {
        return await context.PlanModules.ToListAsync();
    }

    public async Task<IEnumerable<PlanModule>> GetByPlanIdAsync(Guid planId, CancellationToken ct = default)
    {
        return await context.PlanModules
            .AsNoTracking()
            .Where(pm => pm.PlanId == planId && pm.IsEnabled)
            .ToListAsync(ct);
    }

    public async Task<PlanModule?> GetByIdAsync(Guid id)
    {
        return await context.PlanModules.FindAsync(id);
    }

    public async Task<PlanModule> AddAsync(PlanModule planModule)
    {
        context.PlanModules.Add(planModule);
        await context.SaveChangesAsync();
        return planModule;
    }

    public async Task UpdateAsync(PlanModule planModule)
    {
        context.PlanModules.Update(planModule);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await context.PlanModules.FindAsync(id);
        if (entity != null)
        {
            context.PlanModules.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
