using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using scms.Shared.Models;
using scms.Application.Services.SCMS;
using scms.Application.Dtos.SCMS;

namespace scms.API.Controllers;

[Route("api/owner/v1/[controller]")]
[ApiController]
[Authorize(Policy = "OwnerOnly")]
public class ModuleController(IModuleService moduleService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<ModuleDto>>>> GetAll()
    {
        var modules = await moduleService.GetAllModulesAsync();
        return Ok(new ApiResponse<IEnumerable<ModuleDto>>
        {
            Success = true,
            StatusCode = 200,
            Message = "Modules retrieved successfully.",
            Data = modules
        });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<ModuleDto>>> GetById(Guid id)
    {
        var module = await moduleService.GetModuleByIdAsync(id);
        if (module == null)
        {
            return NotFound(new ApiResponse<ModuleDto>
            {
                Success = false,
                StatusCode = 404,
                Message = "Module not found."
            });
        }
        
        return Ok(new ApiResponse<ModuleDto>
        {
            Success = true,
            StatusCode = 200,
            Message = "Module retrieved successfully.",
            Data = module
        });
    }

    [HttpPost]
    public async Task<ActionResult<ApiResponse<ModuleDto>>> Create(CreateModuleDto dto)
    {
        var created = await moduleService.CreateModuleAsync(dto);
        var response = new ApiResponse<ModuleDto>
        {
            Success = true,
            StatusCode = 201,
            Message = "Module created successfully.",
            Data = created
        };
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Update(Guid id, UpdateModuleDto dto)
    {
        var success = await moduleService.UpdateModuleAsync(id, dto);
        if (!success)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 404,
                Message = "Module not found."
            });
        }
        
        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Module updated successfully."
        });
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
    {
        var success = await moduleService.DeleteModuleAsync(id);
        if (!success)
        {
            return NotFound(new ApiResponse<object>
            {
                Success = false,
                StatusCode = 404,
                Message = "Module not found."
            });
        }
        
        return Ok(new ApiResponse<object>
        {
            Success = true,
            StatusCode = 200,
            Message = "Module deleted successfully."
        });
    }
}
