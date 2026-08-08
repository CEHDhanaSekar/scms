namespace scms.Application.Interfaces;

public interface ITenantRepository
{
    /// <summary>Finds a tenant by its normalized domain URL (exact match, case-insensitive).</summary>
    Task<scms.Domain.Entities.SCMS.Tenant?> FindByDomainUrlAsync(string domainUrl, CancellationToken ct = default);

    /// <summary>Returns true when a tenant with the given code exists and is active.</summary>
    Task<bool> ExistsAndActiveAsync(string tenantCode, CancellationToken ct = default);
}
