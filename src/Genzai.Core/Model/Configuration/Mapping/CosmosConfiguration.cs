namespace Genzai.Core.Model.Configuration.Mapping;

/// <summary>
/// Helper cosmos configuration.
/// </summary>
public class CosmosConfiguration
{
    /// <summary>
    /// Config Section name.
    /// </summary>
    public const string Section = "CosmosDbConfiguration";

    /// <summary>
    /// Gets or sets cosmos endpoint.
    /// </summary>
    public string Endpoint { get; set; }

    /// <summary>
    /// Gets or sets cosmos api key.
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    /// Gets or sets cosmos Database Name.
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// Gets or sets cosmos Application Name.
    /// </summary>
    public string ApplicationName { get; set; }

    /// <summary>
    /// Gets or sets collection name.
    /// </summary>
    public string Collection { get; set; }
}