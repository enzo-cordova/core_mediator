namespace Genzai.EfCore.Repository;

/// <summary>
/// Repository class for EfCore.
/// </summary>
/// <typeparam name="TContext">DbContext.</typeparam>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TKey">Key type.</typeparam>
public abstract class Repository<TContext, TEntity, TKey> : IRepository<TEntity, TKey>
    where TContext : DbContext
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Database Context.
    /// </summary>
    protected readonly TContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TContext, TEntity, TKey}"/> class.
    /// </summary>
    /// <param name="context">Database Context.</param>
    protected Repository(TContext context)
    {
        this.context = context;
    }

    ///<inheritdoc/>
    public EntityEntry<TEntity> Add(TEntity entity)
    {
        Guard.IsNotNull(entity, string.Format(
            CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(entity)));

        return this.context.Set<TEntity>().Add(entity);
    }

    ///<inheritdoc/>
    public ValueTask<EntityEntry<TEntity>> AddAsync(
        TEntity entity,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(entity, string.Format(
            CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(entity)));

        return this.context.Set<TEntity>().AddAsync(entity, cancellationToken);
    }

    ///<inheritdoc/>
    public EntityEntry<TEntity> Delete(TEntity entity)
    {
        Guard.IsNotNull(entity, string.Format(
            CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(entity)));

        return this.context.Remove(entity);
    }

    ///<inheritdoc/>
    public IEnumerable<TEntity> GetAll(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true)
    {
        var result = this.ConstructQuery(null!, includes, orderBy, disableTracking);

        return result.ToList();
    }

    ///<inheritdoc/>
    public async Task<IEnumerable<TEntity>> GetAllAsync(
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true,
        CancellationToken cancellationToken = default)
    {
        var result = this.ConstructQuery(null!, includes, orderBy, disableTracking);

        return await result.ToListAsync(cancellationToken);
    }

    ///<inheritdoc/>
    public TEntity GetById(TKey id)
    {
        return context.Set<TEntity>().Find(id);
    }

    ///<inheritdoc/>
    public ValueTask<TEntity> GetByIdAsync(
        TKey id,
        CancellationToken cancellationToken = default)
    {
        return context.Set<TEntity>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    }

    ///<inheritdoc/>
    public IEnumerable<TEntity> GetFiltered(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true)
    {
        Guard.IsNotNull(predicate, string.Format(CultureInfo.InvariantCulture, LocalStrings.ExpressionNotBeNull, nameof(predicate)));

        var result = this.ConstructQuery(predicate, includes, orderBy, disableTracking);

        return result.ToList();
    }

    ///<inheritdoc/>
    public async Task<IEnumerable<TEntity>> GetFilteredAsync(
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(
            predicate,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ExpressionNotBeNull, nameof(predicate)));

        var result = this.ConstructQuery(predicate, includes, orderBy, disableTracking);

        return await result.ToListAsync(cancellationToken);
    }

    ///<inheritdoc/>
    public PagedElements<TEntity> GetPaged(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true)
    {
        Guard.Against<ArgumentException>(
            pageIndex <= 0,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParamEqualOrMoreThanZero, nameof(pageIndex)));
        Guard.Against<ArgumentException>(
            pageSize < 0,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParamMoreThanZero, nameof(pageSize)));
        Guard.IsNotNull(
            predicate,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ExpressionNotBeNull, nameof(predicate)));

        var result = this.ConstructQuery(predicate, includes, orderBy, disableTracking);
        var totalElements = result.Count();
        var localResults = result.Page(pageIndex, pageSize).ToList();

        return new PagedElements<TEntity>(localResults, totalElements);
    }

    ///<inheritdoc/>
    public async Task<PagedElements<TEntity>> GetPagedAsync(
        int pageIndex,
        int pageSize,
        Expression<Func<TEntity, bool>> predicate,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true,
        CancellationToken cancellationToken = default)
    {
        Guard.Against<ArgumentException>(
            pageIndex < 0,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParamEqualOrMoreThanZero, nameof(pageIndex)));
        Guard.Against<ArgumentException>(
            pageSize <= 0,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParamMoreThanZero, nameof(pageSize)));
        Guard.IsNotNull(predicate, string.Format(CultureInfo.InvariantCulture, LocalStrings.ExpressionNotBeNull, nameof(predicate)));

        var result = this.ConstructQuery(predicate, includes, orderBy, disableTracking);
        var totalElements = await result.CountAsync(cancellationToken: cancellationToken).ConfigureAwait(false);
        var localResults = await result.Page(pageIndex, pageSize).ToListAsync(cancellationToken);

        return new PagedElements<TEntity>(localResults, totalElements);
    }

    ///<inheritdoc/>
    public EntityEntry<TEntity> Update(TEntity entity)
    {
        Guard.IsNotNull(entity, string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(entity)));

        return this.context.Attach(entity);
    }

    /// <summary>
    /// Save operations
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> SaveAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

    /// <summary>
    /// Update Entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public bool UpdateEntityIntoDbSet(TEntity entity)
    {
        var result = this.context.Set<TEntity>().Update(entity);
        return result.State == EntityState.Modified;
    }

    /// <summary>
    /// If exists object
    /// </summary>
    /// <param name="id">Exist object</param>
    public virtual bool ExistObject(TKey id)
    {
        return GetById(id) != null;
    }

    /// <summary>
    /// It returns entity DbSet
    /// </summary>
    /// <returns>Entity DbSet</returns>
    public virtual DbSet<TEntity> GetEntityDbSet()
    {
        return context.Set<TEntity>();
    }

    /// <summary>
    /// Construct Query Method.
    /// </summary>
    /// <param name="predicate">Filter Expresison.</param>
    /// <param name="includes">Includes children.</param>
    /// <param name="orderBy">Order By Expression.</param>
    /// <param name="disableTracking">For readonly queries.</param>
    /// <returns>Query.</returns>
    protected IQueryable<TEntity> ConstructQuery(
        Expression<Func<TEntity, bool>> predicate = null!,
        Func<IQueryable<TEntity>, IQueryable<TEntity>> includes = null!,
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null!,
        bool disableTracking = true)
    {
        IQueryable<TEntity> internalQuery = this.context.Set<TEntity>();

        if (disableTracking)
        {
            internalQuery.AsNoTracking();
        }

        if (predicate != null)
        {
            internalQuery = internalQuery.Where(predicate);
        }

        if (includes != null)
        {
            internalQuery = includes(internalQuery);
        }

        if (orderBy != null)
        {
            internalQuery = orderBy(internalQuery);
        }

        return internalQuery;
    }
}