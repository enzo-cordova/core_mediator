namespace Genzai.Core.Integration;

/// <summary>
/// Interface for ServiceBus
/// </summary>
public interface IMessageBus
{
    /// <summary>
    /// Publish message to ServiceBus
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="baseMessage"></param>
    /// <param name="topicName"></param>
    /// <returns></returns>
    Task PublishMessage(string connectionString, BaseMessage baseMessage, string topicName);
}