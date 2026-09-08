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
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ICacheService _cacheService;
    private readonly ICacheKeyFactory _cacheKeyFactory;
    private readonly CacheExpirationProvider _cacheExpiration;

    public EmployeeController(
        IEmployeeService employeeService,
        ICacheService cacheService,
        ICacheKeyFactory cacheKeyFactory,
        CacheExpirationProvider cacheExpiration)
    {
        _employeeService = employeeService;
        _cacheService = cacheService;
        _cacheKeyFactory = cacheKeyFactory;
        _cacheExpiration = cacheExpiration;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct = default)
    {
        var cacheKey = _cacheKeyFactory.Create(TenantCacheEntity.Employee, "all");
        var cachedData = await _cacheService.GetAsync<List<EmployeeDto>>(cacheKey, ct);

        if (cachedData != null)
        {
            return Ok(new ApiResponse<List<EmployeeDto>> { Success = true, StatusCode = 200, Data = cachedData });
        }

        var employees = await _employeeService.GetAllAsync(ct);

        await _cacheService.SetAsync(cacheKey, employees, _cacheExpiration.GetExpiration(), ct);

        return Ok(new ApiResponse<List<EmployeeDto>> { Success = true, StatusCode = 200, Data = employees });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var employee = await _employeeService.GetByIdAsync(id, ct);
        if (employee == null) return NotFound(new ApiResponse<EmployeeDto> { Success = false, StatusCode = 404, Message = "Employee not found" });
        return Ok(new ApiResponse<EmployeeDto> { Success = true, StatusCode = 200, Data = employee });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateEmployeeDto dto, CancellationToken ct)
    {
        var createdBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
        var result = await _employeeService.CreateAsync(dto, createdBy, ct);

        // Invalidate lists cache
        await _cacheService.RemoveAsync(_cacheKeyFactory.Create(TenantCacheEntity.Employee, "all"), ct);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<EmployeeDto> { Success = true, StatusCode = 201, Data = result });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEmployeeDto dto, CancellationToken ct)
    {
        if (id != dto.Id) return BadRequest(new ApiResponse<EmployeeDto> { Success = false, StatusCode = 400, Message = "ID mismatch" });
        
        var updatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
        try
        {
            var result = await _employeeService.UpdateAsync(dto, updatedBy, ct);

            // Invalidate lists cache
            await _cacheService.RemoveAsync(_cacheKeyFactory.Create(TenantCacheEntity.Employee, "all"), ct);

            return Ok(new ApiResponse<EmployeeDto> { Success = true, StatusCode = 200, Data = result });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponse<EmployeeDto> { Success = false, StatusCode = 404, Message = "Employee not found" });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var deletedBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
        var result = await _employeeService.DeleteAsync(id, deletedBy, ct);
        if (!result) return NotFound(new ApiResponse<bool> { Success = false, StatusCode = 404, Message = "Employee not found" });

        // Invalidate lists cache
        await _cacheService.RemoveAsync(_cacheKeyFactory.Create(TenantCacheEntity.Employee, "all"), ct);

        return Ok(new ApiResponse<bool> { Success = true, StatusCode = 200, Message = "Deleted successfully", Data = true });
    }
}
