using AutoMapper;
using SGE.Data.Entities;
using SGE.Shared.DTOs;

namespace SGE.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();

            CreateMap<Position, PositionDto>().ReverseMap();

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName,
                    opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : string.Empty))
                .ForMember(dest => dest.PositionName,
                    opt => opt.MapFrom(src => src.Position != null ? src.Position.Name : string.Empty));

            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
        }
    }
}
