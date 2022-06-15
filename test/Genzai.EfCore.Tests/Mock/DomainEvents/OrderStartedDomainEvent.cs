namespace Genzai.EfCore.Tests.Mock.DomainEvents;

/// <summary>
/// Event when user creates an order.
/// </summary>
public class OrderStartedDomainEvent : INotification
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderStartedDomainEvent"/> class.
    /// </summary>
    /// <param name="order">Order</param>
    /// <param name="userId">User id.</param>
    /// <param name="userName">User name.</param>
    public OrderStartedDomainEvent(Order order, int userId, string userName)
    {
        this.Order = order;
        this.UserId = userId;
        this.UserName = userName;
    }

    /// <summary>
    /// User id.
    /// </summary>
    public int UserId { get; }

    /// <summary>
    /// User Name.
    /// </summary>
    public string UserName { get; }

    /// <summary>
    /// Order entity.
    /// </summary>
    public Order Order { get; }
}