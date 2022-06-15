namespace Genzai.CosmosDb.Model;

/// <summary>
/// Cosmos client configuration.
/// </summary>
public class ClientConfiguration
{
    /// <summary>
    /// Gets or sets cosmos endpoint.
    /// </summary>
    public string? CollectionName { get; set; }

    /// <summary>
    /// Gets or sets cosmos api key.
    /// </summary>
    public string? ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets cosmos Database Name.
    /// </summary>
    public string? DatabaseName { get; set; }

    /// <summary>
    /// Gets or sets cosmos client options.
    /// </summary>
    public MongoClientSettings? Options { get; set; }
}