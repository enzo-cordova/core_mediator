namespace Genzai.CosmosDb.Tests.Fixtures;

/// <summary>
/// Helper cosmos configuration.
/// </summary>
public class CosmosConfiguration
{
    /// <summary>
    /// Gets or sets cosmos endpoint.
    /// </summary>
    public virtual string? CollectionName { get; set; }

    /// <summary>
    /// Gets or sets cosmos api key.
    /// </summary>
    public virtual string? ConnectionString { get; set; }

    /// <summary>
    /// Gets or sets cosmos Database Name.
    /// </summary>
    public virtual string? DatabaseName { get; set; }
}