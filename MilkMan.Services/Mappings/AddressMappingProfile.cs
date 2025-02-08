using AutoMapper;
using MilkMan.Domain.Entities;
using MilkMan.Shared.DTOs.Address;


namespace MilkMan.Application.Mappings
{
    public class AddressMappingProfile : Profile
    {
        public AddressMappingProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<CreateAddressDto, Address>();
            CreateMap<UpdateAddressDto, Address>();

        }
    }
}
