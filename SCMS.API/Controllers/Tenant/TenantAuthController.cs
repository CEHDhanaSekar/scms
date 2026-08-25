using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;
using scms.Shared.Models;

namespace scms.API.Controllers.Tenant;

[Route("api/tenant/v1/auth")]
[ApiController]
public class TenantAuthController(ITenantAuthService tenantAuthService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<TenantAuthResponseDto>>> Login([FromBody] TenantLoginRequestDto request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var response = await tenantAuthService.LoginAsync(request, ipAddress);

        if (response == null)
        {
            return Unauthorized(new ApiResponse<TenantAuthResponseDto>
            {
                Success = false,
                StatusCode = 401,
                Message = "Invalid username/email or password.",
                Data = null
            });
        }

        return Ok(new ApiResponse<TenantAuthResponseDto>
        {
            Success = true,
            StatusCode = 200,
            Message = "Login successful.",
            Data = response
        });
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<ApiResponse<TenantAuthResponseDto>>> RefreshToken([FromBody] TenantRefreshTokenRequestDto request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var response = await tenantAuthService.RefreshTokenAsync(request.RefreshToken, ipAddress);

        if (response == null)
        {
            return BadRequest(new ApiResponse<TenantAuthResponseDto>
            {
                Success = false,
                StatusCode = 400,
                Message = "Invalid or expired refresh token.",
                Data = null
            });
        }

        return Ok(new ApiResponse<TenantAuthResponseDto>
        {
            Success = true,
            StatusCode = 200,
            Message = "Token refreshed successfully.",
            Data = response
        });
    }

    [HttpPost("revoke-token")]
    public async Task<ActionResult<ApiResponse<object>>> RevokeToken([FromBody] TenantRevokeTokenRequestDto request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var success = await tenantAuthService.RevokeTokenAsync(request.RefreshToken, ipAddress);

        if (!success)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 400,
                Message = "Invalid or expired refresh token."
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Token revoked successfully."
        });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<ActionResult<ApiResponse<object>>> Logout([FromBody] TenantLogoutRequestDto request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var success = await tenantAuthService.RevokeTokenAsync(request.RefreshToken, ipAddress);

        if (!success)
        {
            return BadRequest(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 400,
                Message = "Invalid or expired refresh token."
            });
        }

        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Logged out successfully."
        });
    }
}
