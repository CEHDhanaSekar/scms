using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;
using System.Security.Claims;
using scms.Shared.Models;
using scms.Application.Common.Caching;
using scms.Application.Interfaces;
using scms.Infrastructure.Caching;
using SCMS.Domain.Enums;

namespace scms.API.Controllers.Tenant;

[Route("api/tenant/v1/[controller]")]
[ApiController]
[Authorize]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;
    private readonly ICacheService _cacheService;
    private readonly ICacheKeyFactory _cacheKeyFactory;
    private readonly CacheExpirationProvider _cacheExpiration;

    public DepartmentController(
        IDepartmentService departmentService,
        ICacheService cacheService,
        ICacheKeyFactory cacheKeyFactory,
        CacheExpirationProvider cacheExpiration)
    {
        _departmentService = departmentService;
        _cacheService = cacheService;
        _cacheKeyFactory = cacheKeyFactory;
        _cacheExpiration = cacheExpiration;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct = default)
    {
        var cacheKey = _cacheKeyFactory.Create(TenantCacheEntity.Department, "all");
        var cachedData = await _cacheService.GetAsync<List<DepartmentDto>>(cacheKey, ct);

        if (cachedData != null)
        {
            return Ok(new ApiResponse<List<DepartmentDto>> { Success = true, StatusCode = 200, Data = cachedData });
        }

        var departments = await _departmentService.GetAllAsync(ct);

        await _cacheService.SetAsync(cacheKey, departments, _cacheExpiration.GetExpiration(), ct);

        return Ok(new ApiResponse<List<DepartmentDto>> { Success = true, StatusCode = 200, Data = departments });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var department = await _departmentService.GetByIdAsync(id, ct);
        if (department == null) return NotFound(new ApiResponse<DepartmentDto> { Success = false, StatusCode = 404, Message = "Department not found" });
        return Ok(new ApiResponse<DepartmentDto> { Success = true, StatusCode = 200, Data = department });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentDto dto, CancellationToken ct)
    {
        var createdBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
        var result = await _departmentService.CreateAsync(dto, createdBy, ct);

        // Invalidate lists cache
        await _cacheService.RemoveAsync(_cacheKeyFactory.Create(TenantCacheEntity.Department, "all"), ct);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<DepartmentDto> { Success = true, StatusCode = 201, Data = result });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateDepartmentDto dto, CancellationToken ct)
    {
        if (id != dto.Id) return BadRequest(new ApiResponse<DepartmentDto> { Success = false, StatusCode = 400, Message = "ID mismatch" });

        var updatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
        try
        {
            var result = await _departmentService.UpdateAsync(dto, updatedBy, ct);

            // Invalidate lists cache
            await _cacheService.RemoveAsync(_cacheKeyFactory.Create(TenantCacheEntity.Department, "all"), ct);

            return Ok(new ApiResponse<DepartmentDto> { Success = true, StatusCode = 200, Data = result });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponse<DepartmentDto> { Success = false, StatusCode = 404, Message = "Department not found" });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var deletedBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
        var result = await _departmentService.DeleteAsync(id, deletedBy, ct);
        if (!result) return NotFound(new ApiResponse<bool> { Success = false, StatusCode = 404, Message = "Department not found" });

        // Invalidate lists cache
        await _cacheService.RemoveAsync(_cacheKeyFactory.Create(TenantCacheEntity.Department, "all"), ct);

        return Ok(new ApiResponse<bool> { Success = true, StatusCode = 200, Message = "Deleted successfully", Data = true });
    }
}
