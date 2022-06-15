namespace Genzai.Core.Telemetry;
/// <summary>
/// ITelemetryProvider
/// </summary>
public interface ITelemetryProvider
{
    /// <summary>
    /// AddTelemetryEventProperty
    /// </summary>
    /// <param name="property"></param>
    /// <param name="value"></param>
    public void AddEventProperty(string property, string value);

    /// <summary>
    /// TrackEvent
    /// </summary>
    /// <param name="eventName"></param>
    public void TrackEvent(string eventName);
}
