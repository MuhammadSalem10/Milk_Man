

using AutoMapper;
using MilkMan.Shared.DTOs.Order;
using MilkMan.Domain.Entities;
using MilkMan.Shared.DTOs.Auth;

namespace MilkMan.Application.Mappings;


public class OrderMappingProfile : Profile
{
    public OrderMappingProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDto>().ReverseMap();
        CreateMap<OrderItem, OrderItemDetailsDto>().ReverseMap();
        CreateMap<Customer, CustomerSummaryDto>().ReverseMap();
    }
}