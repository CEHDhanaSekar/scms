using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.Interfaces.Tenant;

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
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var permissions = await _permissionService.GetAllAsync(ct);
        return Ok(permissions);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var permission = await _permissionService.GetByIdAsync(id, ct);
        if (permission == null) return NotFound();
        return Ok(permission);
    }
}
