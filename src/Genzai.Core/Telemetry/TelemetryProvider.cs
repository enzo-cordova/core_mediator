using Microsoft.ApplicationInsights;

namespace Genzai.Core.Telemetry;

/// <inheritdoc />
public class TelemetryProvider : ITelemetryProvider
{
    private readonly TelemetryClient _telemetryClient;
    private readonly Dictionary<string, string> _eventProperties;

    /// <summary>
    /// TelemetryProvider
    /// </summary>
    /// <param name="telemetryClient"></param>
    public TelemetryProvider(TelemetryClient telemetryClient)
    {
        _telemetryClient = telemetryClient;
        _eventProperties = new Dictionary<string, string>();
    }


    /// <summary>
    /// AddTelemetryEventProperty
    /// </summary>
    /// <param name="property"></param>
    /// <param name="value"></param>
    public void AddEventProperty(string property, string value)
    {
        if(property != null && !_eventProperties.ContainsKey(property))
            _eventProperties.Add(property, value);
    }


    /// <summary>
    /// TrackEvent
    /// </summary>
    /// <param name="eventName"></param>
    public void TrackEvent(string eventName)
    {
        if (_eventProperties != null)
        {
            _telemetryClient.TrackEvent(eventName, _eventProperties);
            _eventProperties.Clear();
        }
        else
            _telemetryClient.TrackEvent(eventName);
    }
}
