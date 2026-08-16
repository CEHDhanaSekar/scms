namespace scms.Application.Interfaces;

public interface ITenantRepository
{
    /// <summary>Finds a tenant by its normalized domain URL (exact match, case-insensitive).</summary>
    Task<Domain.Entities.SCMS.Tenant?> FindByDomainUrlAsync(string domainUrl, CancellationToken ct = default);

    /// <summary>Returns true when a tenant with the given code exists and is active.</summary>
    Task<bool> ExistsAndActiveAsync(string tenantCode, CancellationToken ct = default);

    /// <summary>Returns true if a tenant with the given code exists, regardless of status.</summary>
    Task<bool> ExistsByCodeAsync(string tenantCode, CancellationToken ct = default);

    Task<IEnumerable<Domain.Entities.SCMS.Tenant>> GetAllAsync(CancellationToken ct = default);
    Task<Domain.Entities.SCMS.Tenant?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Domain.Entities.SCMS.Tenant> AddAsync(Domain.Entities.SCMS.Tenant tenant, CancellationToken ct = default);
    Task UpdateAsync(Domain.Entities.SCMS.Tenant tenant, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
