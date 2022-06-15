namespace Genzai.EfCore.Tests.Mock.Mappings;

/// <summary>
/// Order item mapping.
/// </summary>
public class OrderItemMap : EntityMap<OrderItem, int>
{
    /// <summary>
    /// Configure method.
    /// </summary>
    /// <param name="builder">Builder param.</param>
    public override void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        base.Configure(builder);

        builder.ToTable("OrdersItems");

        builder.Property(a => a.Id).UseIdentityColumn();
        builder.Property(o => o.ProductId);
        builder.Property(o => o.ProductName);
        builder.Property(o => o.UnitPrice);
        builder.Property(o => o.Units);

        builder.HasOne(o => o.Order)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(o => o.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}