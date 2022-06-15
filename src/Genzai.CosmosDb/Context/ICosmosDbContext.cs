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
    /// <param name="databaseId">Database Id or Name.</param>
    /// <param name="containerId">Container Id or Name.</param>
    /// <param name="partitionKey">Partition key.</param>
    /// <param name="options">Request options.</param>
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
    /// <param name="databaseId">Database Id or Name.</param>
    /// <param name="options">Request options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Response</returns>
    Task<IMongoDatabase> EnsureDatabaseCreatedAsync(
        string databaseName, MongoDatabaseSettings Databasesettings = null);

    /// <summary>
    /// Get cosmos container.
    /// </summary>
    /// <param name="containerName"></param>
    /// <returns>Cosmos container.</returns>
    IMongoCollection<BsonDocument> GetCollectionByName(string collectionName);

    IMongoCollection<CosmosEntityDomain>? GetCollectionByName<T>(string collectionName);
}