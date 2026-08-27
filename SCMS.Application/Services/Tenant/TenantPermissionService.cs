using AutoMapper;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;

namespace scms.Application.Services.Tenant;

public class TenantPermissionService : ITenantPermissionService
{
    private readonly ITenantPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public TenantPermissionService(ITenantPermissionRepository permissionRepository, IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    public async Task<TenantPermissionDto?> GetByIdAsync(Guid id, bool onlyActive = false, CancellationToken ct = default)
    {
        var permission = await _permissionRepository.GetByIdAsync(id, onlyActive, ct);
        if (permission == null) return null;
        return _mapper.Map<TenantPermissionDto>(permission);
    }

    public async Task<List<TenantPermissionDto>> GetAllAsync(bool onlyActive = false, CancellationToken ct = default)
    {
        var permissions = await _permissionRepository.GetAllAsync(onlyActive, ct);
        return _mapper.Map<List<TenantPermissionDto>>(permissions);
    }
}
