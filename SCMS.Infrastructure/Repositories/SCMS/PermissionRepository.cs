using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces;
using scms.Infrastructure.Data;
using scms.Domain.Entities.SCMS;

namespace scms.Infrastructure.Repositories.SCMS;

public class PermissionRepository(ScmsDbContext context) : IPermissionRepository
{
    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await context.Permissions.ToListAsync();
    }

    public async Task<Permission?> GetByIdAsync(Guid id)
    {
        return await context.Permissions.FindAsync(id);
    }

    public async Task<Permission> AddAsync(Permission permission)
    {
        context.Permissions.Add(permission);
        await context.SaveChangesAsync();
        return permission;
    }

    public async Task UpdateAsync(Permission permission)
    {
        context.Permissions.Update(permission);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var permission = await context.Permissions.FindAsync(id);
        if (permission != null)
        {
            context.Permissions.Remove(permission);
            await context.SaveChangesAsync();
        }
    }
}
