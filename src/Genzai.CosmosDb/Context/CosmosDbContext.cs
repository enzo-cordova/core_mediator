using MongoDB.Bson;

namespace Genzai.CosmosDb.Context;

/// <summary>
/// Service to wrap cosmos client and database.
/// </summary>
public class CosmosDbContext : ICosmosDbContext
{
    private readonly MongoClient client;

    /// <summary>
    /// Cosmos Db Database.
    /// </summary>
    public IMongoDatabase? Database { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosDbContext"/> class.
    /// Only with cosmos Client, don´t forget to call "EnsureDatabaseCreatedAsync".
    /// </summary>
    /// <param name="client">Cosmos Client</param>
    public CosmosDbContext(MongoClient client)
    {
        this.client = client;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosDbContext"/> class.
    /// </summary>
    /// <param name="client">Cosmos Client.</param>
    /// <param name="database">Cosmos Database.</param>
    public CosmosDbContext(MongoClient client, IMongoDatabase database) : this(client)
    {
        this.Database = database;
    }

    /// <inheritdoc/>
    public async Task CreateMongoCollectionAsync(
            string databaseName,
            string collectionName,
            MongoDatabaseSettings Databasesettings,
            CreateCollectionOptions CollectionOptions,
            CancellationToken cancellationToken = default)
    {
        Guard.IsNotNullNorEmpty(
           databaseName,
           string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNullOrEmpty, nameof(databaseName)));

        Guard.IsNotNullNorEmpty(
           databaseName,
           string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNullOrEmpty, nameof(collectionName)));

        if (this.Database == null)
        {
            await this.EnsureDatabaseCreatedAsync(databaseName, Databasesettings);
        }
        await this.Database!.CreateCollectionAsync(collectionName, CollectionOptions, cancellationToken);

    }

    ///<inheritdoc/>
    public void DeleteDatabaseAsync(CancellationToken cancellationToken = default)
    {
        //await this.client.DropDatabaseAsync(null, cancellationToken);

        this.Database = null;
    }

    /// <inheritdoc/>
    public Task<IMongoDatabase> EnsureDatabaseCreatedAsync(
        string databaseName, MongoDatabaseSettings Databasesettings = null)
    {
        Guard.IsNotNullNorEmpty(
            databaseName,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNullOrEmpty, nameof(databaseName)));

        var result = this.client.GetDatabase(databaseName, settings: Databasesettings);

        this.Database = result;

        return Task.FromResult(result);
    }

    ///<inheritdoc/>
    public IMongoCollection<BsonDocument> GetCollectionByName(string collectionName)
    {
        Guard.IsNotNullNorEmpty(
            collectionName,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNullOrEmpty, nameof(collectionName)));

        return this.Database.GetCollection<BsonDocument>(collectionName);
    }

    ///<inheritdoc/>
    public IMongoCollection<T> GetCollectionByName<T>(string collectionName) where T : CosmosEntityDomain
    {
        Guard.IsNotNullNorEmpty(
           collectionName,
           string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNullOrEmpty, nameof(collectionName)));

        return this.Database.GetCollection<T>(collectionName);
    }
}