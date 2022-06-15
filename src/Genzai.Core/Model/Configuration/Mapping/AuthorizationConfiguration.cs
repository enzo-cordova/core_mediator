namespace Genzai.Core.Model.Configuration.Mapping;

/// <summary>
/// Authorization configuration.
/// </summary>
public class AuthorizationConfiguration
{
    /// <summary>
    /// Config Section name.
    /// </summary>
    public const string Section = "Authorization";

    /// <summary>
    /// Gets or sets resource id.
    /// </summary>
    public string ResourceId { get; set; }

    /// <summary>
    /// Gets or sets instance.
    /// </summary>
    public string Instance { get; set; }

    /// <summary>
    /// Gets or sets tenant id.
    /// </summary>
    public string TenantId { get; set; }

    /// <summary>
    /// Oauth 2 url.
    /// </summary>
    public string Oauth2Url { get; set; }
}