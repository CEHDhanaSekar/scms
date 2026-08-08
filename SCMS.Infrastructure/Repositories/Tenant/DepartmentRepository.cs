using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces.Tenant;
using scms.Domain.Entities.Tenant;
using scms.Infrastructure.Persistence;

namespace scms.Infrastructure.Repositories.Tenant;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly TenantDbContext _context;

    public DepartmentRepository(TenantDbContext context)
    {
        _context = context;
    }

    public Task<Department?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return _context.Departments
            .Include(d => d.Employees)
            .FirstOrDefaultAsync(d => d.Id == id && !d.IsDeleted, ct);
    }

    public Task<List<Department>> GetAllAsync(CancellationToken ct = default)
    {
        return _context.Departments
            .Where(d => !d.IsDeleted)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Department department, CancellationToken ct = default)
    {
        await _context.Departments.AddAsync(department, ct);
    }

    public Task UpdateAsync(Department department, CancellationToken ct = default)
    {
        _context.Departments.Update(department);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Department department, CancellationToken ct = default)
    {
        _context.Departments.Update(department);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }
}
