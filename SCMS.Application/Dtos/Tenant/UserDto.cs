namespace scms.Application.Dtos.Tenant;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public Guid? EmployeeId { get; set; }
    public bool IsDeleted { get; set; }
    
    // We can also return roles here
    public List<Guid> RoleIds { get; set; } = new();
}

public class CreateUserDto
{
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public Guid? EmployeeId { get; set; }
    public List<Guid> RoleIds { get; set; } = new();
}

public class UpdateUserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    public bool IsActive { get; set; }
    public Guid? EmployeeId { get; set; }
    public List<Guid> RoleIds { get; set; } = new();
}
