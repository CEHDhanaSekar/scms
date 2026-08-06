using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.DTOs;
using scms.Application.Services.SCMS;
using scms.Shared.Models;

namespace scms.API.Controllers;

[Route("api/owner/v1[controller]")]
[ApiController]
[Authorize(Policy = "OwnerOnly")]
public class PlanController(IPlanService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<PlanDto>>>> GetAll()
    {
        var result = await service.GetAllPlansAsync();
        return Ok(new ApiResponse<IEnumerable<PlanDto>>
        {
            Success = true,
            StatusCode = 200,
            Message = "Plans retrieved successfully.",
            Data = result
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PlanDto>>> GetById(Guid id)
    {
        var result = await service.GetPlanByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ApiResponse<PlanDto>
            {
                Success = false,
                StatusCode = 404,
                Message = "Plan not found."
            });
        }
        return Ok(new ApiResponse<PlanDto>
        {
            Success = true,
            StatusCode = 200,
            Message = "Plan retrieved successfully.",
            Data = result
        });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<PlanDto>>> Create(CreatePlanDto dto)
    {
        var created = await service.CreatePlanAsync(dto);
        var response = new ApiResponse<PlanDto>
        {
            Success = true,
            StatusCode = 201,
            Message = "Plan created successfully.",
            Data = created
        };
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Update(Guid id, UpdatePlanDto dto)
    {
        var success = await service.UpdatePlanAsync(id, dto);
        if (!success)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 404,
                Message = "Plan not found."
            });
        }
        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Plan updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
    {
        var success = await service.DeletePlanAsync(id);
        if (!success)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 404,
                Message = "Plan not found."
            });
        }
        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Plan deleted successfully."
        });
    }
}
