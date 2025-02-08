using AutoMapper;
using MilkMan.Domain.Entities;
using MilkMan.Shared.DTOs.Product;

namespace MilkMan.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<Product, ProductDetailsDto>().ReverseMap();
            CreateMap<MeasurementUnit, MeasurementUnitDto>();
        }
    }
}
