using AutoMapper;
using MilkMan.Shared.DTOs.Auth;
using MilkMan.Domain.Entities;
using MilkMan.Shared.DTOs.Address;


namespace MilkMan.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Customer, RegisterCustomerDto>().ReverseMap();
            CreateMap<Customer, UpdateCustomerDto>().ReverseMap();
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<Address, AddressSummaryDto>().ReverseMap();

        }
    }
}
