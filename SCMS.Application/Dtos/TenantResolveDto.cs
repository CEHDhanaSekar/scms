namespace scms.Application.Dtos;

/// <summary>
/// Safe, public-facing tenant information returned by the /resolve endpoint.
/// Does not include any internal/sensitive fields.
/// </summary>
public class TenantResolveDto
{
    public string TenantCode { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string? LogoUrl { get; init; }
    public string? DomainUrl { get; init; }
}
