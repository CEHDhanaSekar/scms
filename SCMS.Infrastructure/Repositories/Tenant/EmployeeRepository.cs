using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces.Tenant;
using scms.Domain.Entities.Tenant;
using scms.Infrastructure.Persistence;

namespace scms.Infrastructure.Repositories.Tenant;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly TenantDbContext _context;

    public EmployeeRepository(TenantDbContext context)
    {
        _context = context;
    }

    public Task<Employee?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return _context.Employees
            .Include(e => e.Department)
            .FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted, ct);
    }

    public Task<List<Employee>> GetAllAsync(CancellationToken ct = default)
    {
        return _context.Employees
            .Include(e => e.Department)
            .Where(e => !e.IsDeleted)
            .ToListAsync(ct);
    }

    public async Task AddAsync(Employee employee, CancellationToken ct = default)
    {
        await _context.Employees.AddAsync(employee, ct);
    }

    public Task UpdateAsync(Employee employee, CancellationToken ct = default)
    {
        _context.Employees.Update(employee);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Employee employee, CancellationToken ct = default)
    {
        _context.Employees.Update(employee);
        return Task.CompletedTask;
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }
}
