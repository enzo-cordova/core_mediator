namespace Genzai.Core.Domain.Model;

/// <summary>
/// Entity with events.
/// </summary>
/// <typeparam name="TEntity">Entity Type.</typeparam>
/// <typeparam name="TKey">Key Type.</typeparam>
[ExcludeFromCodeCoverage]
public abstract class EntityWithEvents<TEntity, TKey> : Entity<TEntity, TKey>, IDomainEvent
    where TEntity : Entity<TEntity, TKey>
    where TKey : IEquatable<TKey>
{
    private List<INotification> domainEvents;

    ///<inheritdoc/>
    public IReadOnlyCollection<INotification> DomainEvents => this.domainEvents?.AsReadOnly();

    ///<inheritdoc/>
    public void AddDomainEvent(INotification eventItem)
    {
        Guard.IsNotNull(eventItem, string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(eventItem)));

        (this.domainEvents ??= new List<INotification>()).Add(eventItem);
    }

    ///<inheritdoc/>
    public void ClearDomainEvents()
    {
        this.domainEvents?.Clear();
    }

    ///<inheritdoc/>
    public void RemoveDomainEvent(INotification eventItem)
    {
        Guard.IsNotNull(eventItem, string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(eventItem)));

        (this.domainEvents ??= new List<INotification>()).Remove(eventItem);
    }
}