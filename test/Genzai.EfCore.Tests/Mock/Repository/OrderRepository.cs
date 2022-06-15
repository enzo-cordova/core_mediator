namespace Genzai.EfCore.Tests.Mock.Repository;

/// <summary>
/// Order repository.
/// </summary>
public class OrderRepository : Repository<FakeDomainContext, Order, int>, IOrderRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OrderRepository"/> class.
    /// </summary>
    /// <param name="context"></param>
    public OrderRepository(FakeDomainContext context) : base(context)
    {
    }
}