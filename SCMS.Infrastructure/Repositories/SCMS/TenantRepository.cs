using Microsoft.EntityFrameworkCore;
using scms.Application.Interfaces;
using scms.Infrastructure.Data;

namespace scms.Infrastructure.Repositories.SCMS;

/// <summary>
/// Implements <see cref="ITenantRepository"/> using <see cref="ScmsDbContext"/>
/// (the master/owner database that holds the Tenants table).
/// </summary>
public sealed class TenantRepository : ITenantRepository
{
    private readonly ScmsDbContext _ctx;

    public TenantRepository(ScmsDbContext ctx)
    {
        _ctx = ctx;
    }

    /// <inheritdoc/>
    public Task<Domain.Entities.SCMS.Tenant?> FindByDomainUrlAsync(string domainUrl, CancellationToken ct = default) =>
        _ctx.Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(
                t => t.DomainUrl != null &&
                     t.DomainUrl.ToLower() == domainUrl &&
                     t.IsActive,
                ct);

    /// <inheritdoc/>
    public Task<bool> ExistsAndActiveAsync(string tenantCode, CancellationToken ct = default) =>
        _ctx.Tenants
            .AsNoTracking()
            .AnyAsync(
                t => t.TenantCode.ToLower() == tenantCode && t.IsActive,
                ct);

    /// <inheritdoc/>
    public Task<bool> ExistsByCodeAsync(string tenantCode, CancellationToken ct = default) =>
        _ctx.Tenants
            .AsNoTracking()
            .AnyAsync(
                t => t.TenantCode.ToLower() == tenantCode.ToLower(),
                ct);

    public async Task<IEnumerable<Domain.Entities.SCMS.Tenant>> GetAllAsync(CancellationToken ct = default) =>
        await _ctx.Tenants.ToListAsync(ct);

    public async Task<Domain.Entities.SCMS.Tenant?> GetByIdAsync(Guid id, CancellationToken ct = default) =>
        await _ctx.Tenants.FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<Domain.Entities.SCMS.Tenant> AddAsync(Domain.Entities.SCMS.Tenant tenant, CancellationToken ct = default)
    {
        _ctx.Tenants.Add(tenant);
        await _ctx.SaveChangesAsync(ct);
        return tenant;
    }

    public async Task UpdateAsync(Domain.Entities.SCMS.Tenant tenant, CancellationToken ct = default)
    {
        _ctx.Tenants.Update(tenant);
        await _ctx.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await _ctx.Tenants.FirstOrDefaultAsync(t => t.Id == id, ct);
        if (entity != null)
        {
            _ctx.Tenants.Remove(entity);
            await _ctx.SaveChangesAsync(ct);
        }
    }
}
