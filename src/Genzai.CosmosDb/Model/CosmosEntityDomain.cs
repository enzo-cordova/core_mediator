namespace Genzai.CosmosDb.Model;

/// <summary>
/// Entity for Cosmos with domain events.
/// </summary>
public abstract class CosmosEntityDomain : IDomainEvent
{
    private List<INotification> domainEvents = null;

    ///<inheritdoc/>
    [JsonIgnore]
    public IReadOnlyCollection<INotification> DomainEvents => this.domainEvents?.AsReadOnly();

    ///<inheritdoc/>
    public void AddDomainEvent(INotification eventItem)
    {
        Guard.IsNotNull(eventItem, string.Format(
            CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(eventItem)));

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
        Guard.IsNotNull(eventItem, string.Format(
            CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(eventItem)));

        (this.domainEvents ??= new List<INotification>()).Remove(eventItem);
    }

    ///<inheritdoc/>
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }
}