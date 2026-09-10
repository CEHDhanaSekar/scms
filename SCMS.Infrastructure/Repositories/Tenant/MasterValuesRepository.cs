using Microsoft.EntityFrameworkCore;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;
using scms.Infrastructure.Persistence;

namespace scms.Infrastructure.Repositories.Tenant;

public class MasterValuesRepository : IMasterValuesRepository
{
    private readonly TenantDbContext _context;

    public MasterValuesRepository(TenantDbContext context)
    {
        _context = context;
    }

    public Task<List<MasterValuesDto>> GetAllAsync(CancellationToken ct = default)
    {
        return _context.MasterValues
            .Where(m => m.IsActive)
            .Select(m => new MasterValuesDto
            {
                Id = m.Id,
                Type = m.Type,
                Key = m.Key,
                DisplayName = m.DisplayName,
                Description = m.Description,
                IsActive = m.IsActive
            })
            .ToListAsync(ct);
    }

    public Task<List<MasterValuesDto>> GetByTypeAsync(string type, CancellationToken ct = default)
    {
        return _context.MasterValues
            .Where(m => m.IsActive && m.Type == type)
            .Select(m => new MasterValuesDto
            {
                Id = m.Id,
                Type = m.Type,
                Key = m.Key,
                DisplayName = m.DisplayName,
                Description = m.Description,
                IsActive = m.IsActive
            })
            .ToListAsync(ct);
    }

    public Task<MasterValuesDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return _context.MasterValues
            .Where(m => m.Id == id && m.IsActive)
            .Select(m => new MasterValuesDto
            {
                Id = m.Id,
                Type = m.Type,
                Key = m.Key,
                DisplayName = m.DisplayName,
                Description = m.Description,
                IsActive = m.IsActive
            })
            .FirstOrDefaultAsync(ct);
    }
}
