namespace Genzai.Core.Integration;

/// <summary>
/// IEventBus
/// </summary>
public interface IEventBus
{
    /// <summary>
    /// Publish message to ServiceBus
    /// </summary>
    /// <param name="baseMessage"></param>
    /// <returns></returns>
    Task Publish(BaseMessage baseMessage);
}