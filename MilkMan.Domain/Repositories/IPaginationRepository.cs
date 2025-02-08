

using System.Linq.Expressions;

namespace MilkMan.Domain.Repositories
{
    public interface IPaginationRepository<TEntity> where TEntity : class
    {
        Task<(IEnumerable<TEntity> Items, int TotalCount)> GetPagedAsync(
    int pageNumber,
    int pageSize,
    bool trackChanges = false,
    Expression<Func<TEntity, bool>>? filter = null,
    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
    Expression<Func<TEntity, object>>[]? includes = null);

    }

    public interface IBulkOperationsRepository<TEntity> where TEntity : class
    {
        Task AddRangeAsync(IEnumerable<TEntity> entities);
    }

    public interface ISoftDeleteRepository<TEntity> where TEntity : class
    {
        Task SoftDeleteAsync(int id);
    }
}
