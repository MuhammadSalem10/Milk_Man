using AutoMapper;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Shared.DTOs;
using MilkMan.Application.Interfaces;


namespace MilkMan.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategories(bool trackChanges)
    {
        var categories = await _unitOfWork.Categories.GetAllAsync(trackChanges);
        return _mapper.Map<List<CategoryDto>>(categories);
    }

    public async Task<CategoryDto?> GetCategoryById(int id, bool trackChanges)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id, trackChanges);
        return _mapper.Map<CategoryDto>(category);
    }

    public async Task<CategoryDto> CreateCategory(CreateCategoryDto createCategoryDto)
    {
        var category = _mapper.Map<Category>(createCategoryDto);
        var addedCategory = await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<CategoryDto>(addedCategory);
    }

    public async Task<CategoryDto> UpdateCategory(int id, UpdateCategoryDto updateCategoryDto)
    {
        var existingCategory = await _unitOfWork.Categories.GetByIdAsync(id, true) ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
        _mapper.Map(updateCategoryDto, existingCategory);
        _unitOfWork.Categories.Update(existingCategory);
        await _unitOfWork.CompleteAsync();
        return _mapper.Map<CategoryDto>(existingCategory);
    }

    public async Task DeleteCategory(int id)
    {
        var existingCategory = await _unitOfWork.Categories.GetByIdAsync(id, true) ?? throw new KeyNotFoundException($"Category with ID {id} not found.");
        await _unitOfWork.Categories.DeleteAsync(existingCategory);
        await _unitOfWork.CompleteAsync();
    }

   
}


