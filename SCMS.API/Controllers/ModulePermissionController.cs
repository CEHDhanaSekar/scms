using Microsoft.AspNetCore.Mvc;
using scms.Application.DTOs;
using scms.Application.Services;

namespace scms.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ModulePermissionController(IModulePermissionService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ModulePermissionDto>>> GetAll()
    {
        var result = await service.GetAllModulePermissionsAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ModulePermissionDto>> GetById(Guid id)
    {
        var result = await service.GetModulePermissionByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<ModulePermissionDto>> Create(CreateModulePermissionDto dto)
    {
        var created = await service.CreateModulePermissionAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateModulePermissionDto dto)
    {
        var success = await service.UpdateModulePermissionAsync(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var success = await service.DeleteModulePermissionAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
