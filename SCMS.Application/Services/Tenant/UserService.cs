using AutoMapper;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;
using scms.Domain.Entities.Tenant;

namespace scms.Application.Services.Tenant;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<UserDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user == null) return null;
        return _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetAllAsync(CancellationToken ct = default)
    {
        var users = await _userRepository.GetAllAsync(ct);
        return _mapper.Map<List<UserDto>>(users);
    }

    public async Task<UserDto> CreateAsync(CreateUserDto dto, string createdBy, CancellationToken ct = default)
    {
        var user = _mapper.Map<User>(dto);
        // Password hashing would typically happen here
        user.PasswordHash = dto.Password; // Replace with proper hashing
        user.CreatedAt = DateTime.UtcNow;
        user.CreatedBy = createdBy;
        user.IsActive = true;

        if (dto.RoleIds != null && dto.RoleIds.Any())
        {
            user.UserRoles = dto.RoleIds.Select(roleId => new UserRole { RoleId = roleId }).ToList();
        }

        await _userRepository.AddAsync(user, ct);
        await _userRepository.SaveChangesAsync(ct);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> UpdateAsync(UpdateUserDto dto, string updatedBy, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(dto.Id, ct);
        if (user == null) throw new KeyNotFoundException("User not found");

        _mapper.Map(dto, user);
        user.UpdatedAt = DateTime.UtcNow;
        user.UpdatedBy = updatedBy;

        // Update roles
        user.UserRoles.Clear();
        if (dto.RoleIds != null && dto.RoleIds.Any())
        {
            foreach (var roleId in dto.RoleIds)
            {
                user.UserRoles.Add(new UserRole { Id = Guid.Empty, RoleId = roleId, UserId = user.Id });
            }
        }

        await _userRepository.UpdateAsync(user, ct);
        await _userRepository.SaveChangesAsync(ct);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteAsync(Guid id, string deletedBy, CancellationToken ct = default)
    {
        var user = await _userRepository.GetByIdAsync(id, ct);
        if (user == null) return false;

        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;
        user.DeletedBy = deletedBy;
        user.IsActive = false;

        await _userRepository.DeleteAsync(user, ct);
        await _userRepository.SaveChangesAsync(ct);
        return true;
    }
}
