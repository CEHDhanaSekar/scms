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
    }
}