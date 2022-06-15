namespace Genzai.Core.Model.Configuration.Mapping;

/// <summary>
/// Api manager configuration.
/// </summary>
public class ApiManagerConfiguration
{
    /// <summary>
    /// Config Section name.
    /// </summary>
    public const string Section = "ApiManager";

    /// <summary>
    /// Api manager endpoint list.
    /// </summary>
    public IEnumerable<ApiManagerEndpoint> ApiList { get; set; }
}