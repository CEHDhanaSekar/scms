using Microsoft.AspNetCore.Mvc;
using scms.Application.DTOs;
using scms.Application.Services;
using scms.Shared.Models;

namespace scms.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ModulePermissionController(IModulePermissionService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<ModulePermissionDto>>>> GetAll()
    {
        var result = await service.GetAllModulePermissionsAsync();
        return Ok(new ApiResponse<IEnumerable<ModulePermissionDto>>
        {
            Success = true,
            StatusCode = 200,
            Message = "Module permissions retrieved successfully.",
            Data = result
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ModulePermissionDto>>> GetById(Guid id)
    {
        var result = await service.GetModulePermissionByIdAsync(id);
        if (result == null)
        {
            return NotFound(new ApiResponse<ModulePermissionDto>
            {
                Success = false,
                StatusCode = 404,
                Message = "Module permission not found."
            });
        }
        return Ok(new ApiResponse<ModulePermissionDto>
        {
            Success = true,
            StatusCode = 200,
            Message = "Module permission retrieved successfully.",
            Data = result
        });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ModulePermissionDto>>> Create(CreateModulePermissionDto dto)
    {
        var created = await service.CreateModulePermissionAsync(dto);
        var response = new ApiResponse<ModulePermissionDto>
        {
            Success = true,
            StatusCode = 201,
            Message = "Module permission created successfully.",
            Data = created
        };
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Update(Guid id, UpdateModulePermissionDto dto)
    {
        var success = await service.UpdateModulePermissionAsync(id, dto);
        if (!success)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 404,
                Message = "Module permission not found."
            });
        }
        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Module permission updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
    {
        var success = await service.DeleteModulePermissionAsync(id);
        if (!success)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 404,
                Message = "Module permission not found."
            });
        }
        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Module permission deleted successfully."
        });
    }
}
