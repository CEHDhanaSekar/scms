using Microsoft.AspNetCore.Mvc;
using scms.Application.DTOs;
using scms.Application.Services;

namespace scms.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PlanModuleController(IPlanModuleService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanModuleDto>>> GetAll()
    {
        var result = await service.GetAllPlanModulesAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlanModuleDto>> GetById(Guid id)
    {
        var result = await service.GetPlanModuleByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PlanModuleDto>> Create(CreatePlanModuleDto dto)
    {
        var created = await service.CreatePlanModuleAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdatePlanModuleDto dto)
    {
        var success = await service.UpdatePlanModuleAsync(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var success = await service.DeletePlanModuleAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
