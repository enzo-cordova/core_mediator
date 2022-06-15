namespace Genzai.Core.Integration;

/// <summary>
/// IServiceBusConnection
/// </summary>
public class ServiceBusConnection : IEntryPointSettings
{
    /// <summary>
    /// EndpointSb
    /// </summary>
    public string EndpointSb { get; set; }

    /// <summary>
    /// TopicName
    /// </summary>
    public string TopicName { get; set; }

    /// <summary>
    /// AutoComplete
    /// </summary>
    public bool AutoComplete { get; set; } = false;

    /// <summary>
    /// MaxConcurrentCalls
    /// </summary>
    public int MaxConcurrentCalls { get; set; } = 10;
}