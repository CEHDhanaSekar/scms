using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;
using System.Security.Claims;
using scms.Shared.Models;

namespace scms.API.Controllers.Tenant;

[Route("api/tenant/v1/[controller]")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] bool onlyActive = true, CancellationToken ct = default)
    {
        var users = await _userService.GetAllAsync(onlyActive, ct);
        return Ok(new ApiResponse<List<UserDto>> { Success = true, StatusCode = 200, Data = users });
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, [FromQuery] bool onlyActive = true, CancellationToken ct = default)
    {
        var user = await _userService.GetByIdAsync(id, onlyActive, ct);
        if (user == null) return NotFound(new ApiResponse<UserDto> { Success = false, StatusCode = 404, Message = "User not found" });
        return Ok(new ApiResponse<UserDto> { Success = true, StatusCode = 200, Data = user });
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto dto, CancellationToken ct)
    {
        var createdBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
        var result = await _userService.CreateAsync(dto, createdBy, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, new ApiResponse<UserDto> { Success = true, StatusCode = 201, Data = result });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto dto, CancellationToken ct)
    {
        if (id != dto.Id) return BadRequest(new ApiResponse<UserDto> { Success = false, StatusCode = 400, Message = "ID mismatch" });
        
        var updatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
        try
        {
            var result = await _userService.UpdateAsync(dto, updatedBy, ct);
            return Ok(new ApiResponse<UserDto> { Success = true, StatusCode = 200, Data = result });
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new ApiResponse<UserDto> { Success = false, StatusCode = 404, Message = "User not found" });
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var deletedBy = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "System";
        var result = await _userService.DeleteAsync(id, deletedBy, ct);
        if (!result) return NotFound(new ApiResponse<bool> { Success = false, StatusCode = 404, Message = "User not found" });
        return Ok(new ApiResponse<bool> { Success = true, StatusCode = 200, Message = "Deleted successfully", Data = true });
    }

    [HttpGet("{id:guid}/permissions")]
    public async Task<IActionResult> GetPermissions(Guid id, CancellationToken ct)
    {
        var result = await _userService.GetPermissionsAsync(id, ct);
        if (result == null) return NotFound(new ApiResponse<UserPermissionsResponseDto> { Success = false, StatusCode = 404, Message = "User not found" });
        return Ok(new ApiResponse<UserPermissionsResponseDto> { Success = true, StatusCode = 200, Data = result });
    }
}
