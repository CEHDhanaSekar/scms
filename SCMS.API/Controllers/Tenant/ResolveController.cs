using Microsoft.AspNetCore.Mvc;
using scms.Application.Dtos;
using scms.Application.Services;
using scms.Shared.Models;

namespace scms.API.Controllers.Tenant;

/// <summary>
/// Unauthenticated endpoint called by Angular apps on startup to discover their tenant code.
/// Does NOT require x-tenant-code header — it is the endpoint that provides it.
/// </summary>
[Route("api/tenant/v1/[controller]")]
[ApiController]
public class ResolveController : ControllerBase
{
    private readonly ITenantResolveService _resolveService;

    public ResolveController(ITenantResolveService resolveService)
    {
        _resolveService = resolveService;
    }

    /// <summary>
    /// Resolves the tenant for the requesting Angular app by its Origin (or Referer) header.
    /// Returns 404 when no active tenant matches the origin.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<TenantResolveDto>>> Resolve(
        CancellationToken ct)
    {
        // Extract origin — normalize: strip trailing slash, lowercase
        var origin = Request.Headers["Origin"].FirstOrDefault()
                  ?? Request.Headers["Referer"].FirstOrDefault();

        if (string.IsNullOrWhiteSpace(origin))
        {
            return BadRequest(new ApiResponse<TenantResolveDto>
            {
                Success = false,
                StatusCode = 400,
                Message = "Missing Origin or Referer header."
            });
        }

        var dto = await _resolveService.ResolveByOriginAsync(origin, ct);

        if (dto is null)
        {
            return NotFound(new ApiResponse<TenantResolveDto>
            {
                Success = false,
                StatusCode = 404,
                Message = $"No active tenant found for origin '{origin}'."
            });
        }

        return Ok(new ApiResponse<TenantResolveDto>
        {
            Success = true,
            StatusCode = 200,
            Message = "Tenant resolved successfully.",
            Data = dto
        });
    }
}
