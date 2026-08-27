using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces.Tenant;
using scms.Infrastructure.Persistence;
using scms.Domain.Entities.Tenant;

namespace scms.Infrastructure.Repositories.Tenant;

public class TenantPermissionRepository : ITenantPermissionRepository
{
    private readonly TenantDbContext _context;

    public TenantPermissionRepository(TenantDbContext context)
    {
        _context = context;
    }

    public Task<TenantPermission?> GetByIdAsync(Guid id, bool onlyActive = false, CancellationToken ct = default)
    {
        var query = _context.Permissions.Where(p => p.Id == id);
        if (onlyActive) query = query.Where(p => p.IsActive);
        return query.FirstOrDefaultAsync(ct);
    }

    public Task<List<TenantPermission>> GetAllAsync(bool onlyActive = false, CancellationToken ct = default)
    {
        var query = _context.Permissions.AsQueryable();
        if (onlyActive) query = query.Where(p => p.IsActive);
        return query.ToListAsync(ct);
    }
}
