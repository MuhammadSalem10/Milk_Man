using MilkMan.Shared.DTOs;


namespace MilkMan.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllCategories(bool trackChanges);
        Task<CategoryDto?> GetCategoryById(int id, bool trackChanges);
        Task<CategoryDto> CreateCategory(CreateCategoryDto createCategoryDto);

        Task<CategoryDto> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto);

        Task DeleteCategory(int id);
    }

}
