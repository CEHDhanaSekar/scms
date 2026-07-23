using AutoMapper;
using scms.Application.DTOs;
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

        CreateMap<Plan, PlanDto>();
        CreateMap<CreatePlanDto, Plan>();
        CreateMap<UpdatePlanDto, Plan>();

        CreateMap<PlanModule, PlanModuleDto>();
        CreateMap<CreatePlanModuleDto, PlanModule>();
        CreateMap<UpdatePlanModuleDto, PlanModule>();
    }
}