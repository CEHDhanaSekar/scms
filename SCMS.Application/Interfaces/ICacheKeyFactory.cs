using SCMS.Domain.Enums;

namespace scms.Application.Interfaces;

public interface ICacheKeyFactory
{
    /// <summary>
    /// Builds a tenant-scoped Redis cache key.
    /// Tenant code is resolved automatically from <see cref="scms.Shared.Models.ITenantContext"/>.
    /// Format: <c>{instanceName}{tenantCode}:{entity}:{key}</c>
    /// </summary>
    string Create(TenantCacheEntity entity, object key);
}
