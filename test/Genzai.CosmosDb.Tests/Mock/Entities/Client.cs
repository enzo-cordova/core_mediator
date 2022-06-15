namespace Genzai.CosmosDb.Tests.Mock.Entities;

/// <summary>
/// Client Entity for testing Cosmos DB.
/// </summary>
public class Client : CosmosEntityDomain
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Client"/> class.
    /// </summary>
    /// <param name="clientId">Client Id.</param>
    /// <param name="name">Name.</param>
    /// <param name="lastName">Last Name.</param>
    public Client(string clientId, string name, string lastName)
    {
        this.ClientId = clientId;
        this.Name = name;
        this.LastName = lastName;
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
}