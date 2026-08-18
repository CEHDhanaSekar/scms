using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces;
using scms.Infrastructure.Data;

namespace scms.Infrastructure.Repositories.SCMS;

public class ModulePermissionRepository(ScmsDbContext context) : IModulePermissionRepository
{
    public async Task<IEnumerable<ModulePermission>> GetAllAsync()
    {
        return await context.ModulePermissions.ToListAsync();
    }

    public async Task<IEnumerable<ModulePermission>> GetByModuleIdsAsync(IEnumerable<Guid> moduleIds, CancellationToken ct = default)
    {
        return await context.ModulePermissions
            .AsNoTracking()
            .Where(mp => moduleIds.Contains(mp.ModuleId) && mp.IsActive)
            .ToListAsync(ct);
    }

    public async Task<ModulePermission?> GetByIdAsync(Guid id)
    {
        return await context.ModulePermissions.FindAsync(id);
    }

    public async Task<ModulePermission> AddAsync(ModulePermission modulePermission)
    {
        context.ModulePermissions.Add(modulePermission);
        await context.SaveChangesAsync();
        return modulePermission;
    }

    public async Task UpdateAsync(ModulePermission modulePermission)
    {
        context.ModulePermissions.Update(modulePermission);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await context.ModulePermissions.FindAsync(id);
        if (entity != null)
        {
            context.ModulePermissions.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
