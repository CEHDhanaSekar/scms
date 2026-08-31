using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces.Tenant;
using scms.Domain.Entities.Tenant;
using scms.Infrastructure.Persistence;

namespace scms.Infrastructure.Repositories.Tenant;

public class SpecializationRepository : ISpecializationRepository
{
    private readonly TenantDbContext _context;

    public SpecializationRepository(TenantDbContext context)
    {
        _context = context;
    }

    public Task<Specialization?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return _context.Specializations
            .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted, ct);
    }

    public Task<List<Specialization>> GetAllAsync(CancellationToken ct = default)
    {
        return _context.Specializations
            .Where(s => !s.IsDeleted)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Specialization specialization, CancellationToken ct = default)
    {
        await _context.Specializations.AddAsync(specialization, ct);
    }

    public Task UpdateAsync(Specialization specialization, CancellationToken ct = default)
    {
        _context.Specializations.Update(specialization);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Specialization specialization, CancellationToken ct = default)
    {
        _context.Specializations.Update(specialization);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }
}
