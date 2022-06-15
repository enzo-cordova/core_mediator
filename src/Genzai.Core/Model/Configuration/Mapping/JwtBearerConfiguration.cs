namespace Genzai.Core.Model.Configuration.Mapping;

/// <summary>
/// Jwt bearer configuration.
/// </summary>
public class JwtBearerConfiguration
{
    /// <summary>
    /// Config Section name.
    /// </summary>
    public const string Section = "JWT";

    /// <summary>
    /// Key.
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Valid issuer.
    /// </summary>
    public string Issuer { get; set; }

    /// <summary>
    /// Valid audience.
    /// </summary>
    public string Audience { get; set; }
}