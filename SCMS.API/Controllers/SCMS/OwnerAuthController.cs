using Microsoft.AspNetCore.Mvc;
using scms.Application.Dtos.SCMS;
using scms.Infrastructure.Services;
using scms.Shared.Models;

namespace scms.API.Controllers;

[Route("api/owner/v1/auth")]
[ApiController]

public class OwnerAuthController(IOwnerAuthService ownerAuthService) : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<ApiResponse<OwnerAuthResponseDto>>> Login([FromBody] OwnerLoginRequestDto request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var response = await ownerAuthService.LoginAsync(request, ipAddress);

        if (response == null)
        {
            return Unauthorized(new ApiResponse<OwnerAuthResponseDto>
            {
                Success = false,
                StatusCode = 401,
                Message = "Invalid username/email or password.",
                Data = null
            });
        }

        return Ok(new ApiResponse<OwnerAuthResponseDto>
        {
            Success = true,
            StatusCode = 200,
            Message = "Login successful.",
            Data = response
        });
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<ApiResponse<OwnerAuthResponseDto>>> RefreshToken([FromBody] OwnerRefreshTokenRequestDto request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var response = await ownerAuthService.RefreshTokenAsync(request.RefreshToken, ipAddress);

        if (response == null)
        {
            return BadRequest(new ApiResponse<OwnerAuthResponseDto>
            {
                Success = false,
                StatusCode = 400,
                Message = "Invalid or expired refresh token.",
                Data = null
            });
        }

        return Ok(new ApiResponse<OwnerAuthResponseDto>
        {
            Success = true,
            StatusCode = 200,
            Message = "Token refreshed successfully.",
            Data = response
        });
    }

    [HttpPost("revoke-token")]
    public async Task<ActionResult<ApiResponse<object>>> RevokeToken([FromBody] OwnerRevokeTokenRequestDto request)
    {
        var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();
        var success = await ownerAuthService.RevokeTokenAsync(request.RefreshToken, ipAddress);

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
}
