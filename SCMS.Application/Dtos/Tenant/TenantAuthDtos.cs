namespace scms.Application.Dtos.Tenant;

public class TenantLoginRequestDto
{
    public string UsernameOrEmail { get; set; } = default!;
    public string Password { get; set; } = default!;
}

public class TenantRefreshTokenRequestDto
{
    public string RefreshToken { get; set; } = default!;
}

public class TenantRevokeTokenRequestDto
{
    public string RefreshToken { get; set; } = default!;
}

public class TenantLogoutRequestDto
{
    public string RefreshToken { get; set; } = default!;
}

public record TenantAuthResponseDto(
    string AccessToken,
    DateTime AccessExpiresAt,
    string RefreshToken,
    UserDto User
);
