namespace Genzai.Core.Tests.Mock.Entities;

/// <summary>
/// Car class for testing.
/// </summary>
public class CarClassTest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CarClassTest"/> class.
    /// </summary>
    public CarClassTest()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="CarClassTest"/> class.
    /// </summary>
    /// <param name="id">New Id.</param>
    public CarClassTest(int id)
    {
        this.Id = id;
    }

    /// <summary>
    /// Gets or sets Id member.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets Brand member.
    /// </summary>
    public string Brand { get; set; }

    /// <summary>
    /// Gets or sets Model member.
    /// </summary>
    public string Model { get; set; }
}