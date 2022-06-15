namespace Genzai.Core.Model.Configuration.Entity;

/// <summary>
/// Api swagger Info class, corresponds to Swagger Section.
/// </summary>
public class ApiSwaggerInfo
{
    /// <summary>
    /// Gets or sets swagger Title.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets swagger Description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Gets or sets swagger Version.
    /// </summary>
    public string Version { get; set; }
}