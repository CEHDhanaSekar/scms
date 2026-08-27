using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using scms.Infrastructure.Services;
using scms.Application.Interfaces;
using scms.Shared.Models;

namespace scms.Infrastructure.Extensions;

public static class AuthenticationExtensions
{
    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<OwnerJwtSettings>(configuration.GetSection("Jwt:Owner"));
        services.Configure<JwtSettings>(configuration.GetSection("Jwt:Tenant"));

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer("OwnerScheme")
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme);

        services.AddOptions<JwtBearerOptions>("OwnerScheme")
            .Configure<IOptions<OwnerJwtSettings>>((opts, ownerJwtOptions) =>
            {
                var settings = ownerJwtOptions.Value;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
            .Configure<IOptions<JwtSettings>>((opts, tenantJwtOptions) =>
            {
                var settings = tenantJwtOptions.Value;
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SecretKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("OwnerOnly", policy =>
            {
                policy.AddAuthenticationSchemes("OwnerScheme");
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("UserType", "Owner");
            });

            options.AddPolicy("TenantOnly", policy =>
            {
                policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                policy.RequireAuthenticatedUser();
            });
        });

        return services;
    }

    public static IServiceCollection AddAuthServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasherService, PasswordHasherService>();
        services.AddScoped<IOwnerAuthService, OwnerAuthService>();
        services.AddScoped<Application.Interfaces.Tenant.ITenantAuthService, TenantAuthService>();
        return services;
    }
}