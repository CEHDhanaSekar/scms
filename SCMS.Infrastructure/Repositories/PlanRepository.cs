using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces;
using scms.Infrastructure.Data;

namespace scms.Infrastructure.Repositories;

public class PlanRepository(ScmsDbContext context) : IPlanRepository
{
    public async Task<IEnumerable<Plan>> GetAllAsync()
    {
        return await context.Plans.ToListAsync();
    }

    public async Task<Plan?> GetByIdAsync(Guid id)
    {
        return await context.Plans.FindAsync(id);
    }

    public async Task<Plan> AddAsync(Plan plan)
    {
        context.Plans.Add(plan);
        await context.SaveChangesAsync();
        return plan;
    }

    public async Task UpdateAsync(Plan plan)
    {
        context.Plans.Update(plan);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await context.Plans.FindAsync(id);
        if (entity != null)
        {
            context.Plans.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
