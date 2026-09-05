namespace scms.Infrastructure.Caching;

public interface ICacheKeyFactory
{
    string Create(
        string tenantCode,
        string entity,
        object key);
}