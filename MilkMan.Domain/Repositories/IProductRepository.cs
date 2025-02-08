using MilkMan.Domain.Entities;
using MilkMan.Shared.Parameters;


namespace MilkMan.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByCategory(ProductQueryParameters parameters, bool trackChanges);

        Task<IEnumerable<Product>> SearchProductsByName(string searchTerm);
        Task<Product?> GetProductDetails(int productId, bool trackChanges);
    }
}
