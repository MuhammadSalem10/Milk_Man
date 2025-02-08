using AutoMapper;
using MilkMan.Domain.Entities;
using MilkMan.Shared.DTOs;


namespace MilkMan.Application.Mappings
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile() 
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();
        }
    }
}
