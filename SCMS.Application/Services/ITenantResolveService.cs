using scms.Application.Dtos;

namespace scms.Application.Services;

public interface ITenantResolveService
{
    /// <summary>
    /// Resolves a tenant by normalizing the given origin URL and matching it against
    /// the Tenants table. Returns null when no active tenant matches.
    /// </summary>
    Task<TenantResolveDto?> ResolveByOriginAsync(string originUrl, CancellationToken ct = default);
}
