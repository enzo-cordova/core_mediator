namespace Genzai.CosmosDb.Extensions;

/// <summary>
/// Service collection extensions.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add CosmosDb wrapper.
    /// </summary>
    /// <param name="services">Services collection.</param>
    /// <param name="configuration">Configuration client.</param>
    public static IServiceCollection AddCosmosDb(this IServiceCollection services, ClientConfiguration configuration)
    {
        return services.AddSingleton<ICosmosDbContext>(GetCosmosDbContext(configuration));
    }

    /// <summary>
    /// Get CosmosDb context.
    /// </summary>
    /// <param name="configuration">Cosmos client configuration.</param>
    /// <returns>Cosmos context wrapper.</returns>
    private static CosmosDbContext GetCosmosDbContext(ClientConfiguration configuration)
    {
        Guard.IsNotNull(
            configuration,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(ClientConfiguration)));
        Guard.IsNotNullNorEmpty(
            configuration.ConnectionString,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ObjectIsNullOrEmpty, nameof(ClientConfiguration.ConnectionString)));
        Guard.IsNotNullNorEmpty(
            configuration.DatabaseName,
            string.Format(CultureInfo.InvariantCulture, LocalStrings.ObjectIsNullOrEmpty, nameof(ClientConfiguration.DatabaseName)));

        MongoClient client;

        if (configuration.Options == null)
        {
            client = new MongoClient(configuration.ConnectionString);
        }
        else
        {
            client = new MongoClient(configuration.Options);
        }

        var database = client.GetDatabase(configuration.DatabaseName);

        return new CosmosDbContext(client, database);
    }
}