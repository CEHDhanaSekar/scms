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
    public Task<Tenant?> FindByDomainUrlAsync(string domainUrl, CancellationToken ct = default) =>
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
}
