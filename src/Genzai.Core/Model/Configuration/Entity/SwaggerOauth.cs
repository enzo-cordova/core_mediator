namespace Genzai.Core.Model.Configuration.Entity;

/// <summary>
/// Swagger oauth section.
/// </summary>
public class SwaggerOauth
{
    /// <summary>
    /// App Name Prompt.
    /// </summary>
    public string AppNamePrompt { get; set; }

    /// <summary>
    /// Client ID.
    /// </summary>
    public string ClientId { get; set; }

    /// <summary>
    /// Client secret.
    /// </summary>
    public string ClientSecret { get; set; }

    /// <summary>
    /// Return url.
    /// </summary>
    public string ReturnUrl { get; set; }
}