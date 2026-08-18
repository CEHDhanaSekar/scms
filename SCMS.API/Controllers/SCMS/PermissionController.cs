using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using scms.Shared.Models;
using scms.Application.Services.SCMS;
using scms.Application.Dtos.SCMS;

namespace scms.API.Controllers;

[Route("api/owner/v1/[controller]")]
[ApiController]
[Authorize(Policy = "OwnerOnly")]
public class PermissionController(IPermissionService permissionService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<PermissionDto>>>> GetAll()
    {
        var permissions = await permissionService.GetAllPermissionsAsync();
        return Ok(new ApiResponse<IEnumerable<PermissionDto>>
        {
            Success = true,
            StatusCode = 200,
            Message = "Permissions retrieved successfully.",
            Data = permissions
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<PermissionDto>>> GetById(Guid id)
    {
        var permission = await permissionService.GetPermissionByIdAsync(id);
        if (permission == null)
        {
            return NotFound(new ApiResponse<PermissionDto>
            {
                Success = false,
                StatusCode = 404,
                Message = "Permission not found."
            });
        }
        
        return Ok(new ApiResponse<PermissionDto>
        {
            Success = true,
            StatusCode = 200,
            Message = "Permission retrieved successfully.",
            Data = permission
        });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<PermissionDto>>> Create(CreatePermissionDto dto)
    {
        var created = await permissionService.CreatePermissionAsync(dto);
        var response = new ApiResponse<PermissionDto>
        {
            Success = true,
            StatusCode = 201,
            Message = "Permission created successfully.",
            Data = created
        };
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Update(Guid id, UpdatePermissionDto dto)
    {
        var success = await permissionService.UpdatePermissionAsync(id, dto);
        if (!success)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 404,
                Message = "Permission not found."
            });
        }
        
        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Permission updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
    {
        var success = await permissionService.DeletePermissionAsync(id);
        if (!success)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 404,
                Message = "Permission not found."
            });
        }
        
        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Permission deleted successfully."
        });
    }
}
