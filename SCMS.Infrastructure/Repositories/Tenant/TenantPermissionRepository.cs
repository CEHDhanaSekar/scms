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

    public Task<TenantPermission> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return _context.Permissions.FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public Task<List<TenantPermission>> GetAllAsync(CancellationToken ct = default)
    {
        return _context.Permissions.ToListAsync(ct);
    }
}
