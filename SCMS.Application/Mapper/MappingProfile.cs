using AutoMapper;
using scms.Application.Dtos.SCMS;
using scms.Application.DTOs;
using scms.Domain.Entities.SCMS;

namespace scms.Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Module, ModuleDto>();
        CreateMap<CreateModuleDto, Module>();
        CreateMap<UpdateModuleDto, Module>();

        CreateMap<ModulePermission, ModulePermissionDto>();
        CreateMap<CreateModulePermissionDto, ModulePermission>();
        CreateMap<UpdateModulePermissionDto, ModulePermission>();

        CreateMap<Permission, PermissionDto>();
        CreateMap<CreatePermissionDto, Permission>();
        CreateMap<UpdatePermissionDto, Permission>();

        CreateMap<Plan, PlanDto>();
        CreateMap<CreatePlanDto, Plan>();
        CreateMap<UpdatePlanDto, Plan>()
            .ForMember(dest => dest.PlanModules, opt => opt.Ignore());

        CreateMap<Tenant, TenantDto>();
        CreateMap<CreateTenantDto, Tenant>();
        CreateMap<UpdateTenantDto, Tenant>();

        CreateMap<PlanModule, PlanModuleDto>();
        CreateMap<CreatePlanModuleDto, PlanModule>();
        CreateMap<UpdatePlanModuleDto, PlanModule>();

        CreateMap<OwnerUser, OwnerUserDto>();
    }
}