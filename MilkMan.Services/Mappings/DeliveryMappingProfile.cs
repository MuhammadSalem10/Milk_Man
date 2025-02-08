

using AutoMapper;
using MilkMan.Domain.Entities;
using MilkMan.Shared.DTOs.Delivery;

namespace MilkMan.Application.Mappings
{
    public class DeliveryMappingProfile : Profile
    {
        public DeliveryMappingProfile() 
        {
            CreateMap<Delivery, DeliveryDto>().ReverseMap();

        }
    }
}
