using AutoMapper;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;
using scms.Domain.Entities.Tenant;

namespace scms.Application.Services.Tenant;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public RoleService(IRoleRepository roleRepository, IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task<RoleDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var role = await _roleRepository.GetByIdAsync(id, ct);
        if (role == null) return null;
        return _mapper.Map<RoleDto>(role);
    }

    public async Task<List<RoleDto>> GetAllAsync(CancellationToken ct = default)
    {
        var roles = await _roleRepository.GetAllAsync(ct);
        return _mapper.Map<List<RoleDto>>(roles);
    }

    public async Task<RoleDto> CreateAsync(CreateRoleDto dto, CancellationToken ct = default)
    {
        var role = _mapper.Map<Role>(dto);
        
        if (dto.PermissionIds != null && dto.PermissionIds.Any())
        {
            role.RolePermissions = dto.PermissionIds.Select(permId => new RolePermission { PermissionId = permId }).ToList();
        }

        await _roleRepository.AddAsync(role, ct);
        await _roleRepository.SaveChangesAsync(ct);

        return _mapper.Map<RoleDto>(role);
    }

    public async Task<RoleDto> UpdateAsync(UpdateRoleDto dto, CancellationToken ct = default)
    {
        var role = await _roleRepository.GetByIdAsync(dto.Id, ct);
        if (role == null) throw new KeyNotFoundException("Role not found");

        _mapper.Map(dto, role);

        // Update permissions
        role.RolePermissions.Clear();
        if (dto.PermissionIds != null && dto.PermissionIds.Any())
        {
            foreach (var permId in dto.PermissionIds)
            {
                role.RolePermissions.Add(new RolePermission { PermissionId = permId, RoleId = role.Id });
            }
        }

        await _roleRepository.UpdateAsync(role, ct);
        await _roleRepository.SaveChangesAsync(ct);

        return _mapper.Map<RoleDto>(role);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var role = await _roleRepository.GetByIdAsync(id, ct);
        if (role == null) return false;

        await _roleRepository.DeleteAsync(role, ct);
        await _roleRepository.SaveChangesAsync(ct);
        return true;
    }
}
