using Microsoft.EntityFrameworkCore;

namespace LotteryChecker.Core.Infrastructures;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet;

    protected BaseRepository(DbContext context)
    {
        _dbSet = context.Set<TEntity>();
    }
    public void Create(TEntity entity)
    {
        _dbSet.Add(entity);
        //Context.Entry<TEntity>(entity).State = EntityState.Added;
    }

    public void CreateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.AddRange(entities);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
        // Context.Entry<TEntity>(entity).State = EntityState.Deleted;
    }

    public void Delete(params object[] ids)
    {
        var entity = _dbSet.Find(ids);
        if (entity == null)
            throw new ArgumentException($"{string.Join(";", ids)} not exist in the {typeof(TEntity).Name} table");
        _dbSet.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
        // Context.Entry<TEntity>(entity).State = EntityState.Modified;
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.OrderByDescending(x => x).ToList();
    }

    public TEntity? GetById(object primaryKey)
    {
        return _dbSet.Find(primaryKey);
    }

    public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
    {
        return _dbSet.Where(predicate);
    }

    public IEnumerable<TEntity> GetPaging(IEnumerable<TEntity> orderBy,
        Func<TEntity, bool>? predicate = null,
        int currentPage = 1,
        int pageSize = 10)
    {
        var skipCount = (currentPage - 1) * pageSize;

        IEnumerable<TEntity> results;
        if (predicate != null)
        {
            results = orderBy.Where(predicate).Skip(skipCount).Take(pageSize);
        }
        else
        {
            results = orderBy.Skip(skipCount).Take(pageSize);
        }

        return results;
    }
}