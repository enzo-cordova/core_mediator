namespace Genzai.Core.Tests.Mock.Entities;

/// <summary>
/// Client test class for testing purposes.
/// </summary>
public class ClientTest
{
    /// <summary>
    /// Empty constructor.
    /// </summary>
    public ClientTest()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ClientTest"/> class.
    /// </summary>
    /// <param name="id">Client id.</param>
    /// <param name="name">Client name.</param>
    /// <param name="lastName">Client LastName.</param>
    /// <param name="manager">Client Manager.</param>
    public ClientTest(int id, string name, string lastName, ManagerTest manager)
    {
        this.ClientId = id;
        this.Name = name;
        this.LastName = lastName;
        this.Manager = manager;
    }

    /// <summary>
    /// Gets or sets Client Id.
    /// </summary>
    [Key]
    public int ClientId { get; set; }

    /// <summary>
    /// Gets or sets Client Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets Last Name.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets Manager.
    /// </summary>
    public ManagerTest Manager { get; set; }
}