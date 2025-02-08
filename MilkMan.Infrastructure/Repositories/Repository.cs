using Microsoft.EntityFrameworkCore;
using MilkMan.Domain.Repositories;
using MilkMan.Infrastructure.Data;
using System.Linq.Expressions;

namespace MilkMan.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    protected readonly MilkManDbContext _context;
    private readonly DbSet<TEntity> _table;
    public Repository(MilkManDbContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id, bool trackChanges, Expression<Func<TEntity, bool>>? filter = null, Expression<Func<TEntity, object>>[]? includes = null)
    {
        IQueryable<TEntity> query = _table;

        if (!trackChanges)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }


        try
        {
            return await query.FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("Multiple elements found with the same ID.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving entity: {ex.Message}", ex);
        }
    }


    public async Task<IEnumerable<TEntity>> GetAllAsync(bool trackChanges = false,
        Expression<Func<TEntity, bool>>? filter = null,
        Expression<Func<TEntity, object>>[]? includes = null)
    {
        IQueryable<TEntity> query = _table;

        if (!trackChanges)
        {
            query = query.AsNoTracking();
        }

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (includes != null)
        {
            foreach (var include in includes)
            {
                query.Include(include);
            }
        }

           return await query.ToListAsync();
        
    }


    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _table.AddAsync(entity);
        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        _table.Update(entity);
        return entity;
    }

    public Task DeleteAsync(TEntity entity)
    {
        _table.Remove(entity);
        return Task.CompletedTask;
    }

    public Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        _table.AddRange(entities);
        return Task.CompletedTask;
    }

    public Task RemoveRange(IEnumerable<TEntity> entities)
    {
        _table.RemoveRange(entities);
        return Task.CompletedTask;
    }
}
