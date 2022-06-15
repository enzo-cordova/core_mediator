namespace Genzai.CosmosDb.Repository;

/// <summary>
/// Cosmos repository implementation.
/// </summary>
/// <typeparam name="TEntity">Type entity.</typeparam>
public abstract class Repository<TEntity> : IRepository<TEntity>
    where TEntity : CosmosEntityDomain
{
    /// <summary>
    /// Mediator instance
    /// </summary>
    protected readonly IMediator Mediator;

    /// <summary>
    /// Cosmos DB Context
    /// </summary>
    protected readonly ICosmosDbContext Context;

    /// <summary>
    /// Cosmos DB Container
    /// </summary>
    protected readonly IMongoCollection<TEntity> Collection;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
    /// </summary>
    /// <param name="mediator">Mediator service.</param>
    /// <param name="container">Cosmos container.</param>
    protected Repository(IMediator mediator, IMongoCollection<TEntity> container)
    {
        this.Collection = container;
        this.Mediator = mediator;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class.
    /// </summary>
    /// <param name="mediator">Mediator service.</param>
    /// <param name="context">Cosmos db context wrapper.</param>
    /// <param name="containerId">Container name / id.</param>
    protected Repository(IMediator mediator, ICosmosDbContext context, string containerId)
    {
        this.Context = context;
        this.Collection = (IMongoCollection<TEntity>?)this.Context.GetCollectionByName<TEntity>(containerId);
        this.Mediator = mediator;
    }

    ///<inheritdoc/>
    public async Task AddItemAsync(
        TEntity entity, InsertOneOptions options = null, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(
            entity,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(entity)));

        await this.Collection.InsertOneAsync(entity, options, cancellationToken);

        if (entity.DomainEvents?.Count > 0)
        {
            await this.NotifyDomainEvents(entity);
        }
    }

    ///<inheritdoc/>
    public async Task<DeleteResult> DeleteItemAsync(
        Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullNorEmpty(
            expression.ToString(),
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNullOrEmpty, nameof(expression)));

        return await this.Collection.DeleteOneAsync<TEntity>(expression, cancellationToken);
    }

    ///<inheritdoc/>
    public async Task<TEntity> GetItemAsync(
        Expression<Func<TEntity, bool>> expression)
    {
        Guard.IsNotNullNorEmpty(
            expression.ToString(),
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNullOrEmpty, nameof(expression)));

        return await this.Collection.Find(expression).FirstOrDefaultAsync();
    }

    ///<inheritdoc/>
    public async Task<List<TEntity>> GetItemsAsync()
    {
        return await this.Collection.Find(_ => true).ToListAsync();
    }

    ///<inheritdoc/>
    public async Task<TEntity> UpdateOneAsync(
        TEntity entity,
        FilterDefinition<TEntity> filter,
        UpdateDefinition<TEntity> updateDefinition,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(
            entity,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(entity)));

        var result = this.Collection.FindOneAndUpdate<TEntity>(filter, updateDefinition, null, cancellationToken);

        if (entity.DomainEvents?.Count > 0)
        {
            await this.NotifyDomainEvents(entity);
        }

        return result;
    }

    ///<inheritdoc/>
    public async Task<ReplaceOneResult> UpsertItemAsync(
        TEntity entity, FilterDefinition<TEntity> filter,
        CancellationToken cancellationToken = default)
    {
        Guard.IsNotNull(
            entity,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(entity)));

        var result = await this.Collection.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = true }, cancellationToken);

        if (entity.DomainEvents?.Count > 0)
        {
            await this.NotifyDomainEvents(entity);
        }

        return result;
    }

    /// <summary>
    /// Notify domain events.
    /// </summary>
    /// <param name="entity"></param>
    private Task NotifyDomainEvents(TEntity entity) => this.Mediator.DispatchDomainEventsAsync(entity);
}