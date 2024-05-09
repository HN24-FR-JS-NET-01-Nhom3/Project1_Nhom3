using Microsoft.EntityFrameworkCore;

namespace LotteryChecker.Core.Infrastructures;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DbSet<TEntity> DbSet;
    protected readonly DbContext Context;

    protected BaseRepository(DbContext context)
    {
        Context = context;
        DbSet = context.Set<TEntity>();
    }
    public void Create(TEntity entity)
    {
        DbSet.Add(entity);
        //Context.Entry<TEntity>(entity).State = EntityState.Added;
    }

    public void CreateRange(IEnumerable<TEntity> entities)
    {
        DbSet.AddRange(entities);
    }

    public void Delete(TEntity entity)
    {
        DbSet.Remove(entity);
        // Context.Entry<TEntity>(entity).State = EntityState.Deleted;
    }

    public void Delete(params object[] ids)
    {
        var entity = DbSet.Find(ids);
        if (entity == null)
            throw new ArgumentException($"{string.Join(";", ids)} not exist in the {typeof(TEntity).Name} table");
        DbSet.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        DbSet.Update(entity);
        // Context.Entry<TEntity>(entity).State = EntityState.Modified;
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        DbSet.UpdateRange(entities);
    }

    public IEnumerable<TEntity> GetAll()
    {
        return DbSet.OrderByDescending(x => x).ToList();
    }

    public TEntity? GetById(object primaryKey)
    {
        return DbSet.Find(primaryKey);
    }

    public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
    {
        return DbSet.Where(predicate);
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