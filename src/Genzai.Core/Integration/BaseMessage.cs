namespace Genzai.Core.Integration;

/// <summary>
/// Base Message for ServiceBus
/// </summary>
[ExcludeFromCodeCoverage]
public class BaseMessage
{
    /// <summary>
    /// Message Identifier
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Creation information
    /// </summary>
    public DateTime MessageCreated { get; set; }

    /// <summary>
    /// Constructor without parameters
    /// </summary>
    public BaseMessage()
    {
        Guid = Guid.NewGuid();
        MessageCreated = DateTime.UtcNow;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id"></param>
    /// <param name="createDate"></param>
    [System.Text.Json.Serialization.JsonConstructor]
    public BaseMessage(Guid id, DateTime createDate)
    {
        Guid = id;
        MessageCreated = createDate;
    }
}