using Microsoft.AspNetCore.Mvc;
using MilkMan.API.Utils;
using MilkMan.Shared.DTOs.Product;
using MilkMan.Application.Interfaces;
using MilkMan.Shared.Parameters;
using MilkMan.Domain.Entities;

namespace MilkMan.API.Controllers;


public class ProductsController : ApiController
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsByCategory([FromQuery] ProductQueryParameters parameters)
    {

        var products = await _productService.GetProductsByCategory(parameters, trackChanges : false);

        return Ok(products);

    }

    [HttpGet("search/{searchTerm}")]
    public async Task<IActionResult> SearchProducts(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return BadRequest();
        }

        var matchingProducts = await _productService.SearchProductsByName(searchTerm);

        if (!matchingProducts.Any())
        {
            return NoContent();
        }

        return Ok(matchingProducts);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProductDetails([FromRoute] int id)
    {
        var product = await _productService.GetProductDetails(id, trackChanges: false);
        if (product == null)
        {
            return NotFound();
        }
        return Ok(product);

    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductDto createProductDto, IFormFile file)
    {
        ValidateFileUpload.ValidateImage(file);

            // Check for file presence
            if (file == null || file.Length == 0)
            {
                return BadRequest("Image is required.");
            }

            // Read the byte array from the IFormFile
            byte[] fileBytes;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }


            var createResult = await _productService.CreateProduct(createProductDto, fileBytes, file.FileName);

            if (createResult.IsFailure)
            {
                return BadRequest(createResult.ErrorMessage);
            }

            var addedProduct = createResult.Value;
            return CreatedAtAction(nameof(GetProductDetails), new { id = addedProduct.Id }, addedProduct);
       
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto updateProductDto, IFormFile file)
    {
        ValidateFileUpload.ValidateImage(file);

        if (updateProductDto == null || updateProductDto.Id != id)
        {
            return BadRequest();
        }

        byte[] fileBytes;
        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            fileBytes = memoryStream.ToArray();
        }

        var updateResult = await _productService.UpdateProduct(id, updateProductDto, fileBytes, file.FileName);

        if (updateResult.IsFailure)
        {
            return BadRequest(updateResult.ErrorMessage);

        }

        return Ok(updateResult.Value); 
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await _productService.DeleteProduct(id);

        return NoContent();
    }

  


}