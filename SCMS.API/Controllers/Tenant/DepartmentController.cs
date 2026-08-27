using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;
using System.Security.Claims;
using scms.Shared.Models;

namespace scms.API.Controllers.Tenant;

[Route("api/tenant/v1/[controller]")]
[ApiController]
[Authorize]
public class DepartmentController : ControllerBase
{
    private readonly IDepartmentService _departmentService;

    public DepartmentController(IDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct)
    {
        var departments = await _departmentService.GetAllAsync(ct);
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
        return Ok(new ApiResponse<bool> { Success = true, StatusCode = 200, Message = "Deleted successfully", Data = true });
    }
}
