using MilkMan.Shared.DTOs.Product;
using MilkMan.Shared.Parameters;
using MilkMan.Shared.Common;

namespace MilkMan.Application.Interfaces
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDto>> GetProductsByCategory(ProductQueryParameters parameters, bool trackChanges);
        public Task<ProductDetailsDto> GetProductDetails(int productId, bool trackChanges);
        Task<IEnumerable<ProductDto>> SearchProductsByName(string searchTerm);
        public Task<Result<ProductDto>> CreateProduct(CreateProductDto createProductDto, byte[] fileBytes, string fileName);
        public Task<Result<ProductDetailsDto>> UpdateProduct(int id, UpdateProductDto updateProductDto, byte[] fileBytes, string fileName);
        public Task DeleteProduct(int productId);
    }

}
