namespace scms.Application.Interfaces;

public interface ITenantDbSeeder
{
    Task<(string Username, string RawPassword)> SeedTenantDataAsync(
        string connectionString, 
        string tenantCode,
        string email,
        IEnumerable<string> permissionKeys, 
        string planName, 
        CancellationToken ct = default);
}
