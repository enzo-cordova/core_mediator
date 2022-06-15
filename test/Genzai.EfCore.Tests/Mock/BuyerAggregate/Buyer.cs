namespace Genzai.EfCore.Tests.Mock.BuyerAggregate;

/// <summary>
/// Buyer entity.
/// </summary>
public class Buyer : EntityWithEvents<Buyer, int>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Buyer"/> class.
    /// </summary>
    public Buyer()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Buyer"/> class.
    /// </summary>
    /// <param name="description">Buyer descriptions.</param>
    public Buyer(string description)
    {
        this.Description = description;
    }

    /// <summary>
    /// Buyer Description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Orders list.
    /// </summary>
    public ICollection<Order> Orders { get; set; }
}