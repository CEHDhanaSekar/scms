using AutoMapper;
using scms.Application.Dtos.Tenant;
using scms.Domain.Entities.Tenant;

namespace scms.Application.Mapper;

public class TenantMappingProfile : Profile
{
    public TenantMappingProfile()
    {
        // Department
        CreateMap<Department, DepartmentDto>();
        CreateMap<CreateDepartmentDto, Department>();
        CreateMap<UpdateDepartmentDto, Department>();

        // Employee
        CreateMap<Employee, EmployeeDto>();
        CreateMap<CreateEmployeeDto, Employee>();
        CreateMap<UpdateEmployeeDto, Employee>();

        // User
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.RoleIds, opt => opt.MapFrom(src => src.UserRoles.Select(ur => ur.RoleId)));
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>();

        // Role
        CreateMap<Role, RoleDto>()
            .ForMember(dest => dest.PermissionIds, opt => opt.MapFrom(src => src.RolePermissions.Select(rp => rp.PermissionId)));
        CreateMap<CreateRoleDto, Role>();
        CreateMap<UpdateRoleDto, Role>();

        // Permission
        CreateMap<TenantPermission, TenantPermissionDto>();
    }
}
