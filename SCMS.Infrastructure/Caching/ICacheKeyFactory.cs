namespace scms.Application.Interfaces;

public interface ICacheKeyFactory
{
    string Create(
        string tenantCode,
        string entity,
        object key);
}