using MilkMan.Domain.Entities;
using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;


namespace MilkMan.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly MilkManDbContext _dbContext;

        public CategoryRepository(MilkManDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

       


    }
}
