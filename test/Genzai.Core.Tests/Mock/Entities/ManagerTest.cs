namespace Genzai.Core.Tests.Mock.Entities;

/// <summary>
/// Manager test class for testing purposes.
/// </summary>
public class ManagerTest
{
    /// <summary>
    /// Empty Contructor.
    /// </summary>
    public ManagerTest()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ManagerTest"/> class.
    /// </summary>
    /// <param name="id">Client id.</param>
    /// <param name="name">Client name.</param>
    /// <param name="lastName">Client LastName.</param>
    public ManagerTest(int id, string name, string lastName)
    {
        this.ManagerId = id;
        this.Name = name;
        this.LastName = lastName;
    }

    /// <summary>
    /// Gets or sets Manager Id.
    /// </summary>
    [Key]
    public int ManagerId { get; set; }

    /// <summary>
    /// Gets or sets Client Name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets Last Name.
    /// </summary>
    public string LastName { get; set; }
}