using AutoMapper;
using MilkMan.Shared.Interfaces;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Shared.DTOs.Product;
using MilkMan.Application.Interfaces;
using System.Text.Json;
using MilkMan.Shared.Common;
using MilkMan.Shared.Parameters;


namespace MilkMan.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;

        public ProductService(IFileStorageService fileStorageService, IUnitOfWork unitOfWork, IMapper mapper, 
            ILoggerManager loggerManager)
        {
            _fileStorageService = fileStorageService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }

        public async Task<Result<ProductDto>> CreateProduct(CreateProductDto createProductDto, byte[] fileBytes, string fileName)
        {
            var product = _mapper.Map<Product>(createProductDto);
            var saveFileResult = await _fileStorageService.SaveFileAsync(fileBytes, fileName, "uploads");
            if(saveFileResult.IsFailure)
            {
                return Result<ProductDto>.Failure("Product Image failed to be saved!");
            }
            //if(saveFileResult.Value is not null)
            product.ImageUrl = saveFileResult.Value;

            var addedProduct = await _unitOfWork.Products.AddAsync(product);
            await _unitOfWork.CompleteAsync();
            _loggerManager.Information($"Product {JsonSerializer.Serialize(addedProduct)} has been added.");
            return Result<ProductDto>.Success(_mapper.Map<ProductDto>(addedProduct));

        }



        public async Task<ProductDetailsDto> GetProductDetails(int productId, bool trackChanges)
        {
            var product = await _unitOfWork.Products.GetProductDetails(productId, trackChanges);
            return _mapper.Map<ProductDetailsDto>(product);
        }

        public async Task<IEnumerable<ProductDto>> GetProductsByCategory(ProductQueryParameters parameters, bool trackChanges)
        {
            var products = await _unitOfWork.Products.GetProductsByCategory(parameters, trackChanges);
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<Result<ProductDetailsDto>> UpdateProduct(int id, UpdateProductDto updateProductDto, byte[] imageBytes, string imageName)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id, trackChanges: false) ?? throw new KeyNotFoundException($"Product with ID: {id} not found!");
            var oldFilePath = existingProduct.ImageUrl;

            var updateFileResult = await _fileStorageService.UpdateFileAsync(imageBytes, imageName, oldFilePath, "uploads");
            if(updateFileResult.IsFailure)
            {
                return Result<ProductDetailsDto>.Failure("Failed to update product image");
            }
            existingProduct.ImageUrl = updateFileResult.Value;
            _mapper.Map(updateProductDto, existingProduct);
            _unitOfWork.Products.Update(existingProduct);
            await _unitOfWork.CompleteAsync();
            return Result<ProductDetailsDto>.Success(_mapper.Map<ProductDetailsDto>(existingProduct));
        }

        public async Task DeleteProduct(int id)
        {
            var existingProduct = await _unitOfWork.Products.GetByIdAsync(id, trackChanges: false) ?? throw new KeyNotFoundException($"Product with ID {id} not found.");
            var oldFilePath = existingProduct.ImageUrl;
            await _fileStorageService.DeleteFileAsync(oldFilePath);
            await _unitOfWork.Products.DeleteAsync(existingProduct);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<IEnumerable<ProductDto>> SearchProductsByName(string searchTerm)
        {

            var products = await _unitOfWork.Products.SearchProductsByName(searchTerm);
            return _mapper.Map<List<ProductDto>>(products);

        }

       

    }

}


