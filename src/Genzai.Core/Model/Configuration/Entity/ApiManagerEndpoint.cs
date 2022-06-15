namespace Genzai.Core.Model.Configuration.Entity;

/// <summary>
/// Api manager endpoints configuration.
/// </summary>
public class ApiManagerEndpoint
{
    /// <summary>
    /// Api name.
    /// </summary>
    public string ApiName { get; set; }

    /// <summary>
    /// Api url.
    /// </summary>
    public string ApiUrl { get; set; }

    /// <summary>
    /// Api subscription key.
    /// </summary>
    public string OcpApimSubscriptionKey { get; set; }

    /// <summary>
    /// Endpoints.
    /// </summary>
    public IEnumerable<Endpoint> Endpoints { get; set; }
}