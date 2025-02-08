using AutoMapper;
using MilkMan.Domain.Entities;
using MilkMan.Shared.DTOs.Returns;

namespace MilkMan.Application.Mappings;

public class ReturnRefundProfile : Profile
{
    public ReturnRefundProfile()
    {
        CreateMap<ReturnRequestDto, ReturnRequest>();
        CreateMap<ReturnItemDto, ReturnItem>();
        CreateMap<ReturnRequest, ReturnRequestResponseDto>();
        CreateMap<ReturnItem, ReturnItemResponseDto>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));
        CreateMap<Refund, RefundResponseDto>();

        CreateMap<ReturnRequest, ReturnRequestResponseDto>()
            .ForMember(dest => dest.AssignedDriverId, opt => opt.MapFrom(src => src.AssignedDriverId))
            .ForMember(dest => dest.PickupDate, opt => opt.MapFrom(src => src.PickupDate));
    }
}