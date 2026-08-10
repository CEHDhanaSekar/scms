using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.DTOs;
using scms.Application.Services.SCMS;
using scms.Shared.Models;

namespace scms.API.Controllers;

[Route("api/owner/v1[controller]")]
[ApiController]
[Authorize(Policy = "OwnerOnly")]
public class TenantController(ITenantService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<TenantDto>>>> GetAll()
    {
        var result = await service.GetAllTenantsAsync();
        return Ok(new ApiResponse<IEnumerable<TenantDto>>
        {
            Success = true,
            StatusCode = 200,
            Message = "Tenants retrieved successfully.",
            Data = result
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<TenantDto>>> GetById(Guid id)
    {
        var result = await service.GetTenantByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ApiResponse<TenantDto>
            {
                Success = false,
                StatusCode = 404,
                Message = "Tenant not found."
            });
        }
        return Ok(new ApiResponse<TenantDto>
        {
            Success = true,
            StatusCode = 200,
            Message = "Tenant retrieved successfully.",
            Data = result
        });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<TenantDto>>> Create(CreateTenantDto dto)
    {
        var created = await service.CreateTenantAsync(dto);
        var response = new ApiResponse<TenantDto>
        {
            Success = true,
            StatusCode = 201,
            Message = "Tenant created successfully.",
            Data = created
        };
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Update(Guid id, UpdateTenantDto dto)
    {
        var success = await service.UpdateTenantAsync(id, dto);
        if (!success)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 404,
                Message = "Tenant not found."
            });
        }
        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Tenant updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
    {
        var success = await service.DeleteTenantAsync(id);
        if (!success)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 404,
                Message = "Tenant not found."
            });
        }
        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Tenant deleted successfully."
        });
    }
}
