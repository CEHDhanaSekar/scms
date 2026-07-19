using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces;
using scms.Infrastructure.Data;

namespace scms.Infrastructure.Repositories;

public class ModuleRepository(ScmsDbContext context) : IModuleRepository
{
    public async Task<IEnumerable<Module>> GetAllAsync()
    {
        return await context.Modules.ToListAsync();
    }

    public async Task<Module?> GetByIdAsync(Guid id)
    {
        return await context.Modules.FindAsync(id);
    }

    public async Task<Module> AddAsync(Module module)
    {
        context.Modules.Add(module);
        await context.SaveChangesAsync();
        return module;
    }

    public async Task UpdateAsync(Module module)
    {
        context.Modules.Update(module);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var module = await context.Modules.FindAsync(id);
        if (module != null)
        {
            context.Modules.Remove(module);
            await context.SaveChangesAsync();
        }
    }
}
