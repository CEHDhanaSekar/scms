namespace scms.Shared.Models;

/// <summary>
/// Holds per-request tenant state. Populated at runtime by TenantResolverMiddleware.
/// </summary>
public interface ITenantContext
{
    string TenantCode { get; }
    string ConnectionString { get; }
    bool IsOwner { get; }

    Task ResolveAsync(string tenantCode, CancellationToken ct = default);
}
