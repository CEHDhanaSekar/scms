using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.Dtos.SCMS;
using scms.Application.Services.SCMS;
using scms.Shared.Models;

namespace scms.API.Controllers;

[Route("api/owner/v1/[controller]")]
[ApiController]
[Authorize(Policy = "OwnerOnly")]
public class PlanModuleController(IPlanModuleService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<PlanModuleDto>>>> GetAll()
    {
        var result = await service.GetAllPlanModulesAsync();
        return Ok(new ApiResponse<IEnumerable<PlanModuleDto>>
        {
            Success = true,
            StatusCode = 200,
            Message = "Plan modules retrieved successfully.",
            Data = result
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PlanModuleDto>>> GetById(Guid id)
    {
        var result = await service.GetPlanModuleByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ApiResponse<PlanModuleDto>
            {
                Success = false,
                StatusCode = 404,
                Message = "Plan module not found."
            });
        }
        return Ok(new ApiResponse<PlanModuleDto>
        {
            Success = true,
            StatusCode = 200,
            Message = "Plan module retrieved successfully.",
            Data = result
        });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<PlanModuleDto>>> Create(CreatePlanModuleDto dto)
    {
        var created = await service.CreatePlanModuleAsync(dto);
        var response = new ApiResponse<PlanModuleDto>
        {
            Success = true,
            StatusCode = 201,
            Message = "Plan module created successfully.",
            Data = created
        };
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Update(Guid id, UpdatePlanModuleDto dto)
    {
        var success = await service.UpdatePlanModuleAsync(id, dto);
        if (!success)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 404,
                Message = "Plan module not found."
            });
        }
        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Plan module updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
    {
        var success = await service.DeletePlanModuleAsync(id);
        if (!success)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 404,
                Message = "Plan module not found."
            });
        }
        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Plan module deleted successfully."
        });
    }
}
