using System.Linq.Expressions;


namespace MilkMan.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id, bool trackChanges, Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>[]? includes = null);
        Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges, Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>[]? includes = null);
        Task<TEntity> AddAsync(TEntity entity);
        TEntity Update(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        Task RemoveRange(IEnumerable<TEntity> entities);
    }


}
