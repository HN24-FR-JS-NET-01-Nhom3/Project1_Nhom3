namespace LotteryChecker.Core.Infrastructures;

public interface IBaseRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// Change state of entity to added
    /// </summary>
    /// <param name="entity"></param>
    void Create(TEntity entity);

    /// <summary>
    ///  Change state of entities to added
    /// </summary>
    /// <param name="entities"></param>
    void CreateRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Change state of entity to deleted
    /// </summary>
    /// <param name="entity"></param>
    void Delete(TEntity entity);

    /// <summary>
    /// Change state of entity to deleted
    /// </summary>
    /// <param name="ids"></param>
    void Delete(params object[] ids);


    /// <summary>
    /// Change state of entity to modified
    /// </summary>
    /// <param name="entity"></param>
    void Update(TEntity entity);

    /// <summary>
    /// Change state of entities to modified
    /// </summary>
    /// <param name="entities"></param>
    void UpdateRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Get all from database
    /// </summary>
    /// <returns></returns>
    IEnumerable<TEntity> GetAll();

    TEntity? GetById(object primaryKey);

    IEnumerable<TEntity> Find(Func<TEntity, bool> predicate);

    public IEnumerable<TEntity> GetPaging(IEnumerable<TEntity> orderBy,
        Func<TEntity, bool>? predicate = null,
        int currentPage = 1,
        int pageSize = 10);
}