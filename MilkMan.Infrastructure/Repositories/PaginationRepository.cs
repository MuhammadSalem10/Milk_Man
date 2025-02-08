using Microsoft.EntityFrameworkCore;
using MilkMan.Domain.Repositories;
using System.Linq.Expressions;
using System.Linq;


namespace MilkMan.Infrastructure.Repositories
{
    public class PaginationRepository<TEntity> : IPaginationRepository<TEntity> where TEntity : class
    {
        protected readonly DbContext _context;
        private readonly DbSet<TEntity> _table;
        public PaginationRepository(DbContext context)
        {
            _context = context;
            _table = _context.Set<TEntity>();
        }

        public async Task<(IEnumerable<TEntity> Items, int TotalCount)> GetPagedAsync(
    int pageNumber,
    int pageSize,
    bool trackChanges = false,
    Expression<Func<TEntity, bool>>? filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    Expression<Func<TEntity, object>>[]? includes = null)
        {
            IQueryable<TEntity> query = _table;

            if (!trackChanges)
                query = query.AsNoTracking();

            if (filter != null)
                query = query.Where(filter);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (orderBy != null)
                query = orderBy(query);

            var totalCount = await query.CountAsync();
            var items = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

            return (items, totalCount);
        }

       
    }
}
