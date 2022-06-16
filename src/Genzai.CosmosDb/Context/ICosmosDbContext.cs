using MongoDB.Bson;

namespace Genzai.CosmosDb.Context;

/// <summary>
/// Service to wrap cosmos client and database.
/// </summary>
public interface ICosmosDbContext
{
    /// <summary>
    /// Method for create new container in CosmosDb database.
    /// Maintenance and testing purpose.
    /// </summary>
    /// <param name="databaseName">Database Id or Name.</param>
    /// <param name="collectionName">Container Id or Name.</param>
    /// <param name="Databasesettings">Partition key.</param>
    /// <param name="CollectionOptions">Collection options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>CosmosDb container.</returns>
    Task CreateMongoCollectionAsync(
        string databaseName,
        string collectionName,
        MongoDatabaseSettings Databasesettings,
        CreateCollectionOptions CollectionOptions,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Method for delete database in CosmosDb.
    /// For Testing purposes.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response</returns>
    void DeleteDatabaseAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Method for create database in CosmosDb Service.
    /// Maintenance and testing purpose.
    /// </summary>
    /// <param name="databaseName">Database Id or Name.</param>
    /// <param name="Databasesettings">Request options.</param>
    /// <returns>Response</returns>
    Task<IMongoDatabase> EnsureDatabaseCreatedAsync(
        string databaseName, MongoDatabaseSettings Databasesettings = null);

    /// <summary>
    /// Get cosmos container.
    /// </summary>
    /// <param name="collectionName"></param>
    /// <returns>Cosmos container.</returns>
    IMongoCollection<BsonDocument> GetCollectionByName(string collectionName);

    /// <summary>
    /// Return a collection by its name
    /// </summary>
    /// <typeparam name="TEntity">Type of the Entity</typeparam>
    /// <param name="collectionName">Name of the collection</param>
    /// <returns></returns>
    IMongoCollection<TEntity>? GetCollectionByName<TEntity>(string collectionName)
        where TEntity : CosmosEntityDomain;
}