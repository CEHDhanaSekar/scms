using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.Interfaces.Tenant;
using scms.Shared.Models;
using scms.Application.Dtos.Tenant;
using scms.Application.Common.Caching;
using scms.Application.Interfaces;
using scms.Infrastructure.Caching;
using SCMS.Domain.Enums;

namespace scms.API.Controllers.Tenant;

[Route("api/tenant/v1/[controller]")]
[ApiController]
[Authorize]
public class TenantPermissionController : ControllerBase
{
    private readonly ITenantPermissionService _permissionService;
    private readonly ICacheService _cacheService;
    private readonly ICacheKeyFactory _cacheKeyFactory;
    private readonly CacheExpirationProvider _cacheExpiration;

    public TenantPermissionController(
        ITenantPermissionService permissionService,
        ICacheService cacheService,
        ICacheKeyFactory cacheKeyFactory,
        CacheExpirationProvider cacheExpiration)
    {
        _permissionService = permissionService;
        _cacheService = cacheService;
        _cacheKeyFactory = cacheKeyFactory;
        _cacheExpiration = cacheExpiration;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = true, CancellationToken ct = default)
    {
        var cacheKey = _cacheKeyFactory.Create(TenantCacheEntity.Permission, $"all:active={onlyActive}");
        var cachedData = await _cacheService.GetAsync<List<TenantPermissionDto>>(cacheKey, ct);

        if (cachedData != null)
        {
            return Ok(new ApiResponse<List<TenantPermissionDto>> { Success = true, StatusCode = 200, Data = cachedData });
        }

        var permissions = await _permissionService.GetAllAsync(onlyActive, ct);

        await _cacheService.SetAsync(cacheKey, permissions, _cacheExpiration.GetExpiration(), ct);

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
