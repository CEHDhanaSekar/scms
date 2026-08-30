using scms.Shared.Dtos;

namespace scms.Application.Dtos.Tenant;

public class UserRoleInfoDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}

public class UserDto : BaseDto
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public DateTime? LastLoginAt { get; set; }
    public Guid? EmployeeId { get; set; }
    public bool IsDeleted { get; set; }
    public bool MustChangePassword { get; set; }
    
    // We can also return roles here
    public List<Guid> RoleIds { get; set; } = new();
    public List<UserRoleInfoDto> Roles { get; set; } = new();
}

public class CreateUserDto
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public Guid? EmployeeId { get; set; }
    public List<Guid> RoleIds { get; set; } = new();
}

public class UpdateUserDto : BaseDto
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public Guid? EmployeeId { get; set; }
    public List<Guid> RoleIds { get; set; } = new();
}
