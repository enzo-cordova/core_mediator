namespace Genzai.CosmosDb.Repository;

/// <summary>
/// Cosmos repository contract.
/// </summary>
/// <typeparam name="TEntity">Type entity.</typeparam>
public interface IRepository<TEntity>
    where TEntity : CosmosEntityDomain
{
    Task AddItemAsync(
            TEntity entity, InsertOneOptions options = null, CancellationToken cancellationToken = default);

    Task<DeleteResult> DeleteItemAsync(
        Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

    Task<TEntity> GetItemAsync(
        Expression<Func<TEntity, bool>> expression);

    Task<List<TEntity>> GetItemsAsync();

    Task<TEntity> UpdateOneAsync(
        TEntity entity,
        FilterDefinition<TEntity> filter,
        UpdateDefinition<TEntity> updateDefinition,
        CancellationToken cancellationToken = default);

    Task<ReplaceOneResult> UpsertItemAsync(
       TEntity entity,
       FilterDefinition<TEntity> filter,
       CancellationToken cancellationToken = default);
}