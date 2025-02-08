using Microsoft.AspNetCore.Mvc;
using MilkMan.Shared.DTOs;
using MilkMan.Application.Interfaces;

namespace MilkMan.API.Controllers;


public class CategoriesController : ApiController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCategories()
    {
        var categories = await _categoryService.GetAllCategories(trackChanges: false);

        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryById(id, false);
        if (category == null)
        {
            return NotFound();
        }
        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {

        var addedCategory = await _categoryService.CreateCategory(createCategoryDto);

        return CreatedAtAction(nameof(GetCategoryById), new { id = addedCategory.Id }, addedCategory);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDto updateCategoryDto)
    {
        if(updateCategoryDto == null || updateCategoryDto.Id != id)
        {
            return BadRequest();
        }


        var updatedCategory = await _categoryService.UpdateCategory(id, updateCategoryDto);

        return Ok(updatedCategory);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        await _categoryService.DeleteCategory(id);

        return NoContent();
    }
}




