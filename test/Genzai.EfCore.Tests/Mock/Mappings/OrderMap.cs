namespace Genzai.EfCore.Tests.Mock.Mappings;

/// <summary>
/// Order mapping.
/// </summary>
public class OrderMap : EntityWithEventsMap<Order, int>
{
    /// <summary>
    /// Configure method.
    /// </summary>
    /// <param name="builder">Builder param.</param>
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");

        base.Configure(builder);

        builder.Property(a => a.Id).UseIdentityColumn();
        builder.Property(o => o.Description);
        builder.OwnsOne(o => o.Address, a => a.WithOwner());

        builder.HasOne(o => o.Buyer)
            .WithMany(p => p.Orders)
            .HasForeignKey(o => o.BuyerId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}