namespace Genzai.Core.Integration;

/// <summary>
/// Implementation messaging
/// </summary>
[ExcludeFromCodeCoverage]
public class ServiceBusMessageBus : IMessageBus
{
    /// <summary>
    /// Publish Message Implementation
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="baseMessage"></param>
    /// <param name="topicName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task PublishMessage(string connectionString, BaseMessage baseMessage, string topicName)
    {
        await using var client = new ServiceBusClient(connectionString);

        ServiceBusSender sender = client.CreateSender(topicName);

        var jsonMessage = JsonConvert.SerializeObject(baseMessage);

        var serviceBusMesage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
        {
            CorrelationId = Guid.NewGuid().ToString()
        };

        await sender.SendMessageAsync(serviceBusMesage);

        await sender.CloseAsync();

        await client.DisposeAsync();
    }
}