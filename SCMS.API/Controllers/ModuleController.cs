using Microsoft.AspNetCore.Mvc;
using scms.Application.DTOs;
using scms.Application.Services;

namespace scms.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class ModuleController(IModuleService moduleService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ModuleDto>>> GetAll()
    {
        var modules = await moduleService.GetAllModulesAsync();
        return Ok(modules);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ModuleDto>> GetById(Guid id)
    {
        var module = await moduleService.GetModuleByIdAsync(id);
        if (module == null) return NotFound();
        
        return Ok(module);
    }

    [HttpPost]
    public async Task<ActionResult<ModuleDto>> Create(CreateModuleDto dto)
    {
        var created = await moduleService.CreateModuleAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdateModuleDto dto)
    {
        var success = await moduleService.UpdateModuleAsync(id, dto);
        if (!success) return NotFound();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var success = await moduleService.DeleteModuleAsync(id);
        if (!success) return NotFound();
        
        return NoContent();
    }
}
