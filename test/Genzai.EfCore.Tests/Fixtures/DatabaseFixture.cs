namespace Genzai.EfCore.Tests.Fixtures;

/// <summary>
/// Database fixture.
/// </summary>
public class DatabaseFixture : IDisposable
{
    private bool disposedValue = false;

    /// <summary>
    /// Database context.
    /// </summary>
    public FakeDomainContext Context { get; }

    /// <summary>
    /// Order repository.
    /// </summary>
    public OrderRepository Repository { get; private set; }

    public OrderRepository RepositoryOrder { get; private set; }

    public AuditableOrderRepository RepositoryAuditable { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseFixture"/> class.
    /// </summary>
    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<FakeDomainContext>()
            .UseInMemoryDatabase(databaseName: "EntityTest")
            .Options;

        var mediator = new Mock<IMediator>();

        Context = new FakeDomainContext(options, mediator.Object, ObjectMock.GetHttpContext().HttpContext.User);
        Repository = new OrderRepository(this.Context);
        RepositoryAuditable = new AuditableOrderRepository(Context);

        var buyers = ObjectMock.GetBuyers();

        Context.Buyers.AddRange(buyers);
        Context.SaveChanges();

        var orders = ObjectMock.GetOrders(this.Context.Buyers.Find(1).Id);

        Context.Orders.AddRange(orders);
        Context.SaveChanges();

        var auditablesorders = ObjectMock.GetAuditableOrder(this.Context.Buyers.Find(1).Id);

        Context.AuditableOrders.AddRange(auditablesorders);
        Context.SaveChanges();
    }

    public IEnumerable<Order> PrepareOrders()
    {
        var options = new DbContextOptionsBuilder<FakeDomainContext>()
           .UseInMemoryDatabase(databaseName: "EntityTest1")
           .Options;

        var mediator = new Mock<IMediator>();

        var context = new FakeDomainContext(options, mediator.Object, ObjectMock.GetHttpContext().HttpContext.User);
        RepositoryOrder = new OrderRepository(context);

        var buyers = ObjectMock.GetBuyers();

        context.Buyers.AddRange(buyers);
        context.SaveChanges();

        var orders = ObjectMock.GetOrders(context.Buyers.Find(1).Id);

        context.Orders.AddRange(orders);
        context.SaveChanges();

        return orders;
    }

    /// <summary>
    /// Dispose method.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
    }

    /// <summary>
    /// Dispose method.
    /// </summary>
    /// <param name="disposing">disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                //this.CosmosContext.Database = null;
                Repository = null;
                RepositoryAuditable = null;
                Context.Dispose();
            }

            disposedValue = true;
        }
    }
}