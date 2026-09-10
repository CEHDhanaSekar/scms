using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.Common.Caching;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces;
using scms.Application.Interfaces.Tenant;
using scms.Infrastructure.Caching;
using scms.Shared.Models;
using SCMS.Domain.Enums;

namespace scms.API.Controllers.Tenant;

[Route("api/tenant/v1/[controller]")]
[ApiController]
[Authorize]
public class MasterValuesController : ControllerBase
{
    private readonly IMasterValuesService _masterValuesService;
    private readonly ICacheService _cacheService;
    private readonly ICacheKeyFactory _cacheKeyFactory;
    private readonly CacheExpirationProvider _cacheExpiration;

    public MasterValuesController(
        IMasterValuesService masterValuesService,
        ICacheService cacheService,
        ICacheKeyFactory cacheKeyFactory,
        CacheExpirationProvider cacheExpiration)
    {
        _masterValuesService = masterValuesService;
        _cacheService = cacheService;
        _cacheKeyFactory = cacheKeyFactory;
        _cacheExpiration = cacheExpiration;
    }

    /// <summary>
    /// Returns all active master values across all types.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken ct = default)
    {
        var cacheKey = _cacheKeyFactory.Create(TenantCacheEntity.MasterValues, "all");
        var cached = await _cacheService.GetAsync<List<MasterValuesDto>>(cacheKey, ct);

        if (cached != null)
            return Ok(new ApiResponse<List<MasterValuesDto>> { Success = true, StatusCode = 200, Data = cached });

        var data = await _masterValuesService.GetAllAsync(ct);
        await _cacheService.SetAsync(cacheKey, data, _cacheExpiration.GetExpiration(), ct);

        return Ok(new ApiResponse<List<MasterValuesDto>> { Success = true, StatusCode = 200, Data = data });
    }

    /// <summary>
    /// Returns all active master values filtered by type (e.g. "EmployeeType").
    /// </summary>
    [HttpGet("by-type/{type}")]
    public async Task<IActionResult> GetByType(string type, CancellationToken ct = default)
    {
        var cacheKey = _cacheKeyFactory.Create(TenantCacheEntity.MasterValues, $"type:{type}");
        var cached = await _cacheService.GetAsync<List<MasterValuesDto>>(cacheKey, ct);

        if (cached != null)
            return Ok(new ApiResponse<List<MasterValuesDto>> { Success = true, StatusCode = 200, Data = cached });

        var data = await _masterValuesService.GetByTypeAsync(type, ct);
        await _cacheService.SetAsync(cacheKey, data, _cacheExpiration.GetExpiration(), ct);

        return Ok(new ApiResponse<List<MasterValuesDto>> { Success = true, StatusCode = 200, Data = data });
    }

    /// <summary>
    /// Returns a single master value by its ID.
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
    {
        var cacheKey = _cacheKeyFactory.Create(TenantCacheEntity.MasterValues, id);
        var cached = await _cacheService.GetAsync<MasterValuesDto>(cacheKey, ct);

        if (cached != null)
            return Ok(new ApiResponse<MasterValuesDto> { Success = true, StatusCode = 200, Data = cached });

        var data = await _masterValuesService.GetByIdAsync(id, ct);
        if (data == null)
            return NotFound(new ApiResponse<MasterValuesDto> { Success = false, StatusCode = 404, Message = "MasterValue not found" });

        await _cacheService.SetAsync(cacheKey, data, _cacheExpiration.GetExpiration(), ct);

        return Ok(new ApiResponse<MasterValuesDto> { Success = true, StatusCode = 200, Data = data });
    }
}
