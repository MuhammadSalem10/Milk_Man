using Microsoft.EntityFrameworkCore;
using MilkMan.Shared.Parameters;
using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;

namespace MilkMan.Infrastructure.Repositories
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly MilkManDbContext _dbContext;

        public ProductRepository(MilkManDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(ProductQueryParameters parameters, bool trackChanges)
        {
            var products = _dbContext.Products.Where(p => p.CategoryId == parameters.CategoryId).Include(p => p.Unit).AsQueryable();

            //Sorting 
            if (string.IsNullOrWhiteSpace(parameters.SortBy) == false)
            {
                if (parameters.SortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    products = parameters.IsAscending ? products.OrderBy(p => p.Price) : products.OrderByDescending(p => p.Price);
                }
            }

            //Pagination 
            var skippedResults = (parameters.PageNumber - 1) * parameters.PageSize;

            if (trackChanges)
            {
                return await products.OrderBy(p => p.Id).Skip(skippedResults).Take(parameters.PageSize).ToListAsync();
            }
            else
            {
                return await products.OrderBy(p => p.Id).AsNoTracking().Skip(skippedResults).Take(parameters.PageSize).ToListAsync();
            }
        }


        public async Task<Product?> GetProductDetails(int productId, bool trackChanges)
        {
            var query = _dbContext.Products
                .Include(p => p.Unit)
                .Include(p => p.Category)
                .AsQueryable();

            return await (trackChanges ? query : query.AsNoTracking())
                .FirstOrDefaultAsync(x => x.Id == productId);
        }


        public async Task<IEnumerable<Product>> SearchProductsByName(string searchTerm)
        {
    
            var matchingProducts = _dbContext.Products.AsNoTracking().Where(p => p.Name.Contains(searchTerm));

            return await matchingProducts.ToListAsync();
        }


    }
}
