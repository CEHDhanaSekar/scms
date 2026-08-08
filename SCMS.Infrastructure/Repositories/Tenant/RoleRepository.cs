using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces.Tenant;
using scms.Domain.Entities.Tenant;
using scms.Infrastructure.Persistence;

namespace scms.Infrastructure.Repositories.Tenant;

public class RoleRepository : IRoleRepository
{
    private readonly TenantDbContext _context;

    public RoleRepository(TenantDbContext context)
    {
        _context = context;
    }

    public Task<Role?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return _context.Roles
            .Include(r => r.RolePermissions)
            .FirstOrDefaultAsync(r => r.Id == id, ct);
    }

    public Task<List<Role>> GetAllAsync(CancellationToken ct = default)
    {
        return _context.Roles
            .Include(r => r.RolePermissions)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Role role, CancellationToken ct = default)
    {
        await _context.Roles.AddAsync(role, ct);
    }

    public Task UpdateAsync(Role role, CancellationToken ct = default)
    {
        _context.Roles.Update(role);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Role role, CancellationToken ct = default)
    {
        _context.Roles.Remove(role); // Or soft delete if added to entity
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }
}
