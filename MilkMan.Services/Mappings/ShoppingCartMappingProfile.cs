using AutoMapper;
using MilkMan.Shared.DTOs.Product;
using MilkMan.Shared.DTOs.ShoppingCart;
using MilkMan.Domain.Entities;


namespace MilkMan.Application.Mappings;
public class ShoppingCartMappingProfile : Profile
{
    public ShoppingCartMappingProfile()
    {
        CreateMap<ShoppingCart, ShoppingCartDto>().ReverseMap(); 
        CreateMap<CartItem, CartItemDto>().ReverseMap(); 
        CreateMap<Product, ProductDto>().ReverseMap(); 
    }
}
