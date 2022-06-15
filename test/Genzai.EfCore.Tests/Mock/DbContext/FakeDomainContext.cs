using System.Security.Claims;

namespace Genzai.EfCore.Tests.Mock.DbContext;

/// <summary>
/// Fake domain context.
/// </summary>
public class FakeDomainContext : ContextDataBase<FakeDomainContext>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FakeDomainContext"/> class.
    /// </summary>
    /// <param name="options">DbContext options.</param>
    /// <param name="mediator">Mediator Service.</param>
    /// <param name="claimsPrincipal">Current principal.</param>
    public FakeDomainContext(DbContextOptions<FakeDomainContext> options, IMediator mediator, ClaimsPrincipal claimsPrincipal)
        : base(options, mediator, claimsPrincipal)
    {
    }

    /// <summary>
    /// Buyers.
    /// </summary>
    public DbSet<Buyer> Buyers { get; set; }

    /// <summary>
    /// Orders table.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Auditable
    /// </summary>
    public DbSet<AuditableOrder> AuditableOrders { get; set; }

    /// <summary>
    /// Order Items table.
    /// </summary>
    public DbSet<OrderItem> OrderItems { get; set; }

    /// <summary>
    /// On model creating method.
    /// </summary>
    /// <param name="modelBuilder">Model builder.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BuyerMap());
        modelBuilder.ApplyConfiguration(new OrderMap());
        modelBuilder.ApplyConfiguration(new OrderItemMap());
        modelBuilder.ApplyConfiguration(new AuditableOrderMap());
    }
}