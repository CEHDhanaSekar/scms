using scms.Application.Dtos.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface ITenantAuthService
{
    Task<TenantAuthResponseDto?> LoginAsync(TenantLoginRequestDto dto, string? ipAddress = null);
    Task<TenantAuthResponseDto?> RefreshTokenAsync(string refreshToken, string? ipAddress = null);
    Task<bool> RevokeTokenAsync(string refreshToken, string? ipAddress = null);
}
