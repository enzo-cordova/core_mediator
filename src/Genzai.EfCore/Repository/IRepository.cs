namespace Genzai.EfCore.Repository;

/// <summary>
/// Repository contract for database operations.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TKey"></typeparam>
public interface IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Add Entity.
    /// </summary>
    /// <param name="entity">Entity type.</param>
    /// <returns>TEntity.</returns>
    EntityEntry<TEntity> Add(TEntity entity);

    /// <summary>
    /// AddAsync Entity.
    /// </summary>
    /// <param name="entity">Entity type.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>TEntity.</returns>
    ValueTask<EntityEntry<TEntity>> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete Entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <returns>TEntity.</returns>
    EntityEntry<TEntity> Delete(TEntity entity);

    /// <summary>
    /// GetAll Entities.
    /// </summary>
    /// <param name="includes">Includes children.</param>
    /// <param name="orderBy">Order By Expression.</param>
    /// <param name="disableTracking">For readonly queries.</param>
    /// <returns>List of entities.</returns>
    IEnumerable<TEntity> GetAll(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true);

    /// <summary>
    /// GetAllAsync Entities.
    /// </summary>
    /// <param name="includes">Includes children.</param>
    /// <param name="orderBy">Order By Expression.</param>
    /// <param name="disableTracking">For readonly queries.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of entities.</returns>
    Task<IEnumerable<TEntity>> GetAllAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// GetById Method.
    /// </summary>
    /// <param name="id">Entity Id.</param>
    /// <returns>Entity.</returns>
    TEntity GetById(TKey id);

    /// <summary>
    /// GetByIdAsync Method.
    /// </summary>
    /// <param name="id">Entity Id.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Entity.</returns>
    ValueTask<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken = default);

    /// <summary>
    /// GetFiltered Method.
    /// </summary>
    /// <param name="predicate">Filter Expresison.</param>
    /// <param name="includes">Includes children.</param>
    /// <param name="orderBy">Order By Expression.</param>
    /// <param name="disableTracking">For readonly queries.</param>
    /// <returns>List of entities.</returns>
    IEnumerable<TEntity> GetFiltered(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true);

    /// <summary>
    /// GetFilteredAsync Method.
    /// </summary>
    /// <param name="predicate">Filter Expresison.</param>
    /// <param name="includes">Includes children.</param>
    /// <param name="orderBy">Order By Expression.</param>
    /// <param name="disableTracking">For readonly queries.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of entities.</returns>
    Task<IEnumerable<TEntity>> GetFilteredAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// GetPaged method.
    /// </summary>
    /// <param name="pageIndex">Page index.</param>
    /// <param name="pageSize">Page Size.</param>
    /// <param name="predicate">Filter Expresison.</param>
    /// <param name="includes">Includes children.</param>
    /// <param name="orderBy">Order By Expression.</param>
    /// <param name="disableTracking">For readonly queries.</param>
    /// <returns>List of entities.</returns>
    PagedElements<TEntity> GetPaged(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true);

    /// <summary>
    /// GetPagedAsync method.
    /// </summary>
    /// <param name="pageIndex">Page index.</param>
    /// <param name="pageSize">Page Size.</param>
    /// <param name="predicate">Filter Expresison.</param>
    /// <param name="includes">Includes children.</param>
    /// <param name="orderBy">Order By Expression.</param>
    /// <param name="disableTracking">For readonly queries.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>List of entities.</returns>
    Task<PagedElements<TEntity>> GetPagedAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Update Entity.
    /// </summary>
    /// <param name="entity">Entity.</param>
    /// <returns>TEntity.</returns>
    EntityEntry<TEntity> Update(TEntity entity);

    /// <summary>
    /// Saves the asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> SaveAsync(CancellationToken cancellationToken); 

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    bool UpdateEntityIntoDbSet(TEntity entity);

    /// <summary>
    /// If exists object
    /// </summary>
    /// <param name="id">Exist object</param>
    bool ExistObject(TKey id);

    /// <summary>
    /// It returns entity db set
    /// </summary>
    /// <returns>Enitty db set</returns>
    DbSet<TEntity> GetEntityDbSet();
}