namespace Genzai.CosmosDb.Tests.Mock.Entities;

/// <summary>
/// Client with events testing class.
/// </summary>
public class ClientWithEvents : CosmosEntityDomain
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientWithEvents"/> class.
    /// </summary>
    /// <param name="clientId">Client Id.</param>
    /// <param name="name">Name.</param>
    /// <param name="lastName">Last Name.</param>
    /// <param name="userId">User id.</param>
    /// <param name="userName">User name.</param>
    public ClientWithEvents(string clientId, string name, string lastName, int userId, string userName)
    {
        this.ClientId = clientId;
        this.Name = name;
        this.LastName = lastName;

        this.AddClientStartedEvent(userId, userName);
    }

    /// <summary>
    /// Gets or Sets Client id.
    /// </summary>
    [JsonProperty(PropertyName = "id")]
    public string ClientId { get; set; }

    /// <summary>
    /// Gets or Sets Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or Sets Last Name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// To String override.
    /// </summary>
    /// <returns>Json Entity.</returns>
    public override string ToString()
    {
        return JsonConvert.SerializeObject(this);
    }

    /// <summary>
    /// Add client started Event.
    /// </summary>
    /// <param name="userId">User id.</param>
    /// <param name="userName">User name.</param>
    private void AddClientStartedEvent(int userId, string userName)
    {
        var domainEvent = new ClientStartedDomainEvent(this, userId, userName);

        this.AddDomainEvent(domainEvent);
    }
}