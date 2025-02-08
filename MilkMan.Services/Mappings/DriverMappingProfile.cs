using AutoMapper;
using MilkMan.Domain.Entities;
using MilkMan.Shared.DTOs.Driver;


namespace MilkMan.Application.Mappings
{
    public class DriverMappingProfile : Profile
    {
        public DriverMappingProfile() 
        {
            CreateMap<Driver, DriverDto>().ReverseMap();
            CreateMap<CreateDriverDto, Driver>();
            CreateMap<UpdateDriverDto,  Driver>();
        }
    }
}
