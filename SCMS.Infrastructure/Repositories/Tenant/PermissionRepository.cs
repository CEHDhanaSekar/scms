using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces.Tenant;
using scms.Domain.Entities.SCMS;
using scms.Infrastructure.Persistence;

namespace scms.Infrastructure.Repositories.Tenant;

public class PermissionRepository : IPermissionRepository
{
    private readonly TenantDbContext _context;

    public PermissionRepository(TenantDbContext context)
    {
        _context = context;
    }

    public Task<Permission?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return _context.Permissions.FirstOrDefaultAsync(p => p.Id == id, ct);
    }

    public Task<List<Permission>> GetAllAsync(CancellationToken ct = default)
    {
        return _context.Permissions.ToListAsync(ct);
    }
}
