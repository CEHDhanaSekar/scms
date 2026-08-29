using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.Interfaces.Tenant;
using scms.Shared.Models;
using scms.Application.Dtos.Tenant;

namespace scms.API.Controllers.Tenant;

[Route("api/tenant/v1/[controller]")]
[ApiController]
[Authorize]
public class TenantPermissionController : ControllerBase
{
    private readonly ITenantPermissionService _permissionService;

    public TenantPermissionController(ITenantPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = true, CancellationToken ct = default)
    {
        var permissions = await _permissionService.GetAllAsync(onlyActive, ct);
        return Ok(new ApiResponse<List<TenantPermissionDto>> { Success = true, StatusCode = 200, Data = permissions });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, [FromQuery] bool onlyActive = true, CancellationToken ct = default)
    {
        var permission = await _permissionService.GetByIdAsync(id, onlyActive, ct);
        if (permission == null) return NotFound(new ApiResponse<TenantPermissionDto> { Success = false, StatusCode = 404, Message = "Permission not found" });
        return Ok(new ApiResponse<TenantPermissionDto> { Success = true, StatusCode = 200, Data = permission });
    }
}
