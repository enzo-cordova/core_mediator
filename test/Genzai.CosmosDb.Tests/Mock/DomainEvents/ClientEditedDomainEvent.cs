namespace Genzai.CosmosDb.Tests.Mock.DomainEvents;

/// <summary>
/// Client edited domain event.
/// </summary>
public class ClientEditedDomainEvent : INotification
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientEditedDomainEvent"/> class.
    /// </summary>
    /// <param name="clientWithEvents">Order</param>
    /// <param name="userId">User id.</param>
    /// <param name="userName">User name.</param>
    public ClientEditedDomainEvent(ClientWithEvents clientWithEvents, int userId, string userName)
    {
        this.UserId = userId;
        this.UserName = userName;
        this.Client = clientWithEvents;
    }

    /// <summary>
    /// User id.
    /// </summary>
    public int UserId { get; }

    /// <summary>
    /// User Name.
    /// </summary>
    public string UserName { get; }

    /// <summary>
    /// Order entity.
    /// </summary>
    public ClientWithEvents Client { get; }
}