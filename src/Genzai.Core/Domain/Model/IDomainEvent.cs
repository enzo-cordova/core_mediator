namespace Genzai.Core.Domain.Model;

/// <summary>
/// Domain Event contract.
/// </summary>
public interface IDomainEvent
{
    /// <summary>W
    /// Domain events.
    /// </summary>
    IReadOnlyCollection<INotification> DomainEvents
    {
        get;
    }

    /// <summary>
    /// Add domain event.
    /// </summary>
    /// <param name="eventItem">Event item.</param>
    void AddDomainEvent(INotification eventItem);

    /// <summary>
    /// Clear domain events.
    /// </summary>
    void ClearDomainEvents();

    /// <summary>
    /// Remove domain event.
    /// </summary>
    /// <param name="eventItem">Event item.</param>
    void RemoveDomainEvent(INotification eventItem);
}