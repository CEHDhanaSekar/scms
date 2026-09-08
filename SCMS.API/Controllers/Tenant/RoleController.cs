using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;
using scms.Shared.Models;
using scms.Application.Common.Caching;
using scms.Application.Interfaces;
using scms.Infrastructure.Caching;
using SCMS.Domain.Enums;

namespace scms.API.Controllers.Tenant;

[Route("api/tenant/v1/[controller]")]
[ApiController]
[Authorize]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly ICacheService _cacheService;
    private readonly ICacheKeyFactory _cacheKeyFactory;
    private readonly CacheExpirationProvider _cacheExpiration;

    public RoleController(
        IRoleService roleService,
        ICacheService cacheService,
        ICacheKeyFactory cacheKeyFactory,
        CacheExpirationProvider cacheExpiration)
    {
        _roleService = roleService;
        _cacheService = cacheService;
        _cacheKeyFactory = cacheKeyFactory;
        _cacheExpiration = cacheExpiration;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = true, CancellationToken ct = default)
    {
        var cacheKey = _cacheKeyFactory.Create(TenantCacheEntity.Role, $"all:active={onlyActive}");
        var cachedData = await _cacheService.GetAsync<List<RoleDto>>(cacheKey, ct);

        if (cachedData != null)
        {
            return Ok(new ApiResponse<List<RoleDto>> { Success = true, StatusCode = 200, Data = cachedData });
        }

        var roles = await _roleService.GetAllAsync(onlyActive, ct);

        await _cacheService.SetAsync(cacheKey, roles, _cacheExpiration.GetExpiration(), ct);

        return Ok(new ApiResponse<List<RoleDto>> { Success = true, StatusCode = 200, Data = roles });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, [FromQuery] bool onlyActive = true, CancellationToken ct = default)
    {
        var role = await _roleService.GetByIdAsync(id, onlyActive, ct);
        if (role == null) return NotFound(new ApiResponse<RoleDto> { Success = false, StatusCode = 404, Message = "Role not found" });
        return Ok(new ApiResponse<RoleDto> { Success = true, StatusCode = 200, Data = role });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleDto dto, CancellationToken ct)
    {
        var result = await _roleService.CreateAsync(dto, ct);

        // Invalidate lists cache
        await _cacheService.RemoveAsync(_cacheKeyFactory.Create(TenantCacheEntity.Role, "all:active=True"), ct);
        await _cacheService.RemoveAsync(_cacheKeyFactory.Create(TenantCacheEntity.Role, "all:active=False"), ct);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<RoleDto> { Success = true, StatusCode = 201, Data = result });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateRoleDto dto, CancellationToken ct)
    {
        if (id != dto.Id) return BadRequest(new ApiResponse<RoleDto> { Success = false, StatusCode = 400, Message = "ID mismatch" });
        
        try
        {
            var result = await _roleService.UpdateAsync(dto, ct);

            // Invalidate lists cache
            await _cacheService.RemoveAsync(_cacheKeyFactory.Create(TenantCacheEntity.Role, "all:active=True"), ct);
            await _cacheService.RemoveAsync(_cacheKeyFactory.Create(TenantCacheEntity.Role, "all:active=False"), ct);

            return Ok(new ApiResponse<RoleDto> { Success = true, StatusCode = 200, Data = result });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponse<RoleDto> { Success = false, StatusCode = 404, Message = "Role not found" });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var result = await _roleService.DeleteAsync(id, ct);
        if (!result) return NotFound(new ApiResponse<bool> { Success = false, StatusCode = 404, Message = "Role not found" });

        // Invalidate lists cache
        await _cacheService.RemoveAsync(_cacheKeyFactory.Create(TenantCacheEntity.Role, "all:active=True"), ct);
        await _cacheService.RemoveAsync(_cacheKeyFactory.Create(TenantCacheEntity.Role, "all:active=False"), ct);

        return Ok(new ApiResponse<bool> { Success = true, StatusCode = 200, Message = "Deleted successfully", Data = true });
    }
}
