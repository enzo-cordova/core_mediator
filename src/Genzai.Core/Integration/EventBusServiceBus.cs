namespace Genzai.Core.Integration;

/// <summary>
/// EventBusServiceBus
/// </summary>
public class EventBusServiceBus : IEventBus, IDisposable
{
    private readonly ServiceBusSender _sender;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceBusConnection"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public EventBusServiceBus(ServiceBusConnection serviceBusConnection)
    {
        var serviceBusConnection1 = serviceBusConnection;
        var subscriptionClient = new ServiceBusClient(serviceBusConnection1.EndpointSb);
        _sender = subscriptionClient.CreateSender(serviceBusConnection1.TopicName);
    }

    /// <summary>
    /// Publish
    /// </summary>
    /// <param name="baseMessage"></param>
    public async Task Publish(BaseMessage baseMessage)
    {
        using ServiceBusMessageBatch messageBatch = await _sender.CreateMessageBatchAsync();
        var json = JsonConvert.SerializeObject(baseMessage, Formatting.Indented);
        if (!messageBatch.TryAddMessage(new ServiceBusMessage(json)))
        {
            throw new ArgumentOutOfRangeException($"The message {json} is too large to fit in the batch.");
        }
        await _sender.SendMessagesAsync(messageBatch);
    }

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual async void Dispose(bool disposing)
    {
        if (disposing)
        {
            await _sender.DisposeAsync().ConfigureAwait(false);
        }
    }
}