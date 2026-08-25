using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces;
using scms.Application.Interfaces.Tenant;
using scms.Domain.Entities.Tenant;
using scms.Infrastructure.Persistence;
using scms.Shared.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace scms.Infrastructure.Services;

public class TenantAuthService(
    TenantDbContext dbContext,
    IPasswordHasherService passwordHasher,
    IMapper mapper,
    IOptions<JwtSettings> jwtOptions) : ITenantAuthService
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public async Task<TenantAuthResponseDto?> LoginAsync(TenantLoginRequestDto dto, string? ipAddress = null)
    {
        if (string.IsNullOrWhiteSpace(dto.UsernameOrEmail) || string.IsNullOrWhiteSpace(dto.Password))
            return null;

        var normalizedInput = dto.UsernameOrEmail.Trim().ToLowerInvariant();

        var user = await dbContext.Users
            .FirstOrDefaultAsync(u => u.Username.ToLower() == normalizedInput || u.Email.ToLower() == normalizedInput);

        if (user == null || !user.IsActive)
            return null;

        if (!passwordHasher.VerifyPassword(dto.Password, user.PasswordHash))
            return null;

        user.LastLoginAt = DateTime.UtcNow;

        var (accessToken, accessExpiresAt) = GenerateAccessToken(user);
        var refreshToken = GenerateRefreshToken(user.Id, ipAddress);

        dbContext.RefreshTokens.Add(refreshToken);
        await dbContext.SaveChangesAsync();

        var userDto = mapper.Map<UserDto>(user);

        return new TenantAuthResponseDto(
            accessToken,
            accessExpiresAt,
            refreshToken.Token,
            userDto
        );
    }

    public async Task<TenantAuthResponseDto?> RefreshTokenAsync(string refreshToken, string? ipAddress = null)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return null;

        var existingToken = await dbContext.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == refreshToken);

        if (existingToken == null || !existingToken.IsActive || !existingToken.User.IsActive)
            return null;

        existingToken.RevokedAt = DateTime.UtcNow;

        var (newAccessToken, accessExpiresAt) = GenerateAccessToken(existingToken.User);
        var newRefreshToken = GenerateRefreshToken(existingToken.UserId, ipAddress);

        existingToken.ReplacedByToken = newRefreshToken.Token;

        dbContext.RefreshTokens.Add(newRefreshToken);
        await dbContext.SaveChangesAsync();

        var userDto = mapper.Map<UserDto>(existingToken.User);

        return new TenantAuthResponseDto(
            newAccessToken,
            accessExpiresAt,
            newRefreshToken.Token,
            userDto
        );
    }

    public async Task<bool> RevokeTokenAsync(string refreshToken, string? ipAddress = null)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return false;

        var token = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(r => r.Token == refreshToken);

        if (token == null || !token.IsActive)
            return false;

        token.RevokedAt = DateTime.UtcNow;
        await dbContext.SaveChangesAsync();
        return true;
    }

    private (string token, DateTime expiresAt) GenerateAccessToken(User user)
    {
        var secretKey = string.IsNullOrWhiteSpace(_jwtSettings.SecretKey)
            ? "default_tenant_secret_key_that_is_at_least_64_bytes_long_for_security_1234567890"
            : _jwtSettings.SecretKey;

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var expiresAt = DateTime.UtcNow.AddMinutes(
            _jwtSettings.AccessTokenExpirationMinutes > 0 ? _jwtSettings.AccessTokenExpirationMinutes : 30);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.GivenName, user.Username),
            new Claim("UserType", "Tenant")
        };

        var token = new JwtSecurityToken(
            issuer: string.IsNullOrWhiteSpace(_jwtSettings.Issuer) ? "SCMS_TENANT" : _jwtSettings.Issuer,
            audience: string.IsNullOrWhiteSpace(_jwtSettings.Audience) ? "SCMS_TENANT_API" : _jwtSettings.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: creds
        );

        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }

    private RefreshToken GenerateRefreshToken(Guid userId, string? ipAddress)
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        var tokenString = Convert.ToBase64String(randomBytes);

        var days = _jwtSettings.RefreshTokenExpirationDays > 0 ? _jwtSettings.RefreshTokenExpirationDays : 7;

        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Token = tokenString,
            ExpiresAt = DateTime.UtcNow.AddDays(days)
        };
    }
}
