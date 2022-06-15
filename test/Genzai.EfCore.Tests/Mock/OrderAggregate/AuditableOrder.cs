namespace Genzai.EfCore.Tests.Mock.OrderAggregate;

/// <summary>
/// Auditable
/// </summary>
public class AuditableOrder : AuditableEntity<AuditableOrder, int>, IAuditable
{
    //private readonly List<OrderItem> orderItems;

    /// <summary>
    /// Initializes a new instance of the <see cref="Order"/> class.
    /// </summary>
    public AuditableOrder()
    {
        this.OrderItems = new List<OrderItem>();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Order"/> class.
    /// </summary>
    /// <param name="buyerId">Buyer id.</param>
    /// <param name="description">Description.</param>
    /// <param name="address">Address.</param>
    /// <param name="userId">User id.</param>
    /// <param name="userName">User name.</param>
    public AuditableOrder(int buyerId, string description, Address address, int userId, string userName)
        : this()
    {
        BuyerId = buyerId;
        Description = description;
        Address = address;

        OrderItems = new HashSet<OrderItem>();
    }

    /// <summary>
    /// Buyer id.
    /// </summary>
    public int BuyerId { get; set; }

    /// <summary>
    /// Description.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Address.
    /// </summary>
    public Address Address { get; }

    /// <summary>
    /// Buyer class.
    /// </summary>
    public Buyer Buyer { get; set; }

    /// <summary>
    /// Order item collections.
    /// </summary>
    public virtual ICollection<OrderItem> OrderItems { get; set; }

    /// <summary>
    /// Add order item.
    /// </summary>
    /// <param name="productId">Product id.</param>
    /// <param name="productName">Product name.</param>
    /// <param name="unitPrice">Unit price.</param>
    /// <param name="units">Units number.</param>
    public void AddOrderItem(int productId, string productName, decimal unitPrice, int units = 1)
    {
        var existingOrder = this.OrderItems.SingleOrDefault(x => x.ProductId == productId);

        if (existingOrder == null)
        {
            this.OrderItems.Add(new OrderItem(productId, productName, unitPrice, units));
        }
        else
        {
            existingOrder.AddUnits(units);
        }
    }
}