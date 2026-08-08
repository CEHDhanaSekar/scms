using AutoMapper;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;

namespace scms.Application.Services.Tenant;

public class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public PermissionService(IPermissionRepository permissionRepository, IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    public async Task<PermissionDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var permission = await _permissionRepository.GetByIdAsync(id, ct);
        if (permission == null) return null;
        return _mapper.Map<PermissionDto>(permission);
    }

    public async Task<List<PermissionDto>> GetAllAsync(CancellationToken ct = default)
    {
        var permissions = await _permissionRepository.GetAllAsync(ct);
        return _mapper.Map<List<PermissionDto>>(permissions);
    }
}
