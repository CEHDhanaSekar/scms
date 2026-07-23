using Microsoft.AspNetCore.Mvc;
using scms.Application.DTOs;
using scms.Application.Services;

namespace scms.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class PlanController(IPlanService service) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlanDto>>> GetAll()
    {
        var result = await service.GetAllPlansAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlanDto>> GetById(Guid id)
    {
        var result = await service.GetPlanByIdAsync(id);
        if (result == null) return NotFound();
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<PlanDto>> Create(CreatePlanDto dto)
    {
        var created = await service.CreatePlanAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(Guid id, UpdatePlanDto dto)
    {
        var success = await service.UpdatePlanAsync(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var success = await service.DeletePlanAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
}
