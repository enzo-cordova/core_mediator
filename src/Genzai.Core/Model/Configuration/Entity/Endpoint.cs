namespace Genzai.Core.Model.Configuration.Entity;

/// <summary>
/// Endpoint name and method to call in Api manager.
/// </summary>
public class Endpoint
{
    /// <summary>
    /// Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Method.
    /// </summary>
    public string Method { get; set; }
}