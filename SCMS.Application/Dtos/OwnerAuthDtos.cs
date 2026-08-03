namespace scms.Application.Dtos;

public record OwnerLoginRequestDto(
    string UsernameOrEmail,
    string Password
);

public record OwnerUserDto(
    Guid Id,
    string Name,
    string Username,
    string Email,
    string Mobile,
    bool IsActive,
    DateTime? LastLoginAt
);

public record OwnerAuthResponseDto(
    string AccessToken,
    DateTime AccessTokenExpiresAt,
    string RefreshToken,
    OwnerUserDto User
);

public record OwnerRefreshTokenRequestDto(
    string RefreshToken
);

public record OwnerRevokeTokenRequestDto(
    string RefreshToken
);
