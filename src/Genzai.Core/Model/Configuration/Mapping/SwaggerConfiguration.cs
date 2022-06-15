namespace Genzai.Core.Model.Configuration.Mapping;

/// <summary>
/// Swagger config.
/// </summary>
public class SwaggerConfiguration
{
    /// <summary>
    /// Config Section name.
    /// </summary>
    public const string Section = "Swagger";

    /// <summary>
    /// Gets or sets environment.
    /// </summary>
    public string Environment { get; set; }

    /// <summary>
    /// Gets or sets name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets apiInfo.
    /// </summary>
    public ApiSwaggerInfo ApiInfo { get; set; }

    /// <summary>
    /// Gets or sets endpoint.
    /// </summary>
    public string Endpoint { get; set; }

    /// <summary>
    /// Oauth parameters.
    /// </summary>
    public SwaggerOauth Oauth { get; set; }
}