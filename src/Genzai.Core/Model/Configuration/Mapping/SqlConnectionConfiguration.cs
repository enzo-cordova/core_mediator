namespace Genzai.Core.Model.Configuration.Mapping;

/// <summary>
/// Sql connection string.
/// </summary>
public class SqlConnectionConfiguration
{
    /// <summary>
    /// Config Section name.
    /// </summary>
    public const string Section = "ConnectionStrings";

    /// <summary>
    /// Gets or sets app Connection.
    /// </summary>
    public string AppConnection { get; set; }

    /// <summary>
    /// Redis connection string
    /// </summary>
    public string Redis { get; set; }
}