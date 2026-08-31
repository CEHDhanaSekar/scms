using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;
using scms.Shared.Models;
using System.Security.Claims;

namespace scms.API.Controllers.Tenant;

[Route("api/tenant/v1/[controller]")]
[ApiController]
[Authorize]
public class SpecializationController : ControllerBase
{
    private readonly ISpecializationService _specializationService;

    public SpecializationController(ISpecializationService specializationService)
    {
        _specializationService = specializationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct = default)
    {
        var specializations = await _specializationService.GetAllAsync(ct);
        return Ok(new ApiResponse<List<SpecializationDto>> { Success = true, StatusCode = 200, Data = specializations });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
    {
        var specialization = await _specializationService.GetByIdAsync(id, ct);
        if (specialization == null) return NotFound(new ApiResponse<SpecializationDto> { Success = false, StatusCode = 404, Message = "Specialization not found" });
        return Ok(new ApiResponse<SpecializationDto> { Success = true, StatusCode = 200, Data = specialization });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSpecializationDto dto, CancellationToken ct)
    {
        var createdBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
        var result = await _specializationService.CreateAsync(dto, createdBy, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<SpecializationDto> { Success = true, StatusCode = 201, Data = result });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSpecializationDto dto, CancellationToken ct)
    {
        if (id != dto.Id) return BadRequest(new ApiResponse<SpecializationDto> { Success = false, StatusCode = 400, Message = "ID mismatch" });

        var updatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
        try
        {
            var result = await _specializationService.UpdateAsync(dto, updatedBy, ct);
            return Ok(new ApiResponse<SpecializationDto> { Success = true, StatusCode = 200, Data = result });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponse<SpecializationDto> { Success = false, StatusCode = 404, Message = "Specialization not found" });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var deletedBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
        var result = await _specializationService.DeleteAsync(id, deletedBy, ct);
        if (!result) return NotFound(new ApiResponse<bool> { Success = false, StatusCode = 404, Message = "Specialization not found" });
        return Ok(new ApiResponse<bool> { Success = true, StatusCode = 200, Message = "Deleted successfully", Data = true });
    }
}
