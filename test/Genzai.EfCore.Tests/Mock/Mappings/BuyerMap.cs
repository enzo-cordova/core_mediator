namespace Genzai.EfCore.Tests.Mock.Mappings;

/// <summary>
/// Buyer map.
/// </summary>
public class BuyerMap : EntityWithEventsMap<Buyer, int>
{
    /// <summary>
    /// Configure method.
    /// </summary>
    /// <param name="builder">Builder param.</param>
    public override void Configure(EntityTypeBuilder<Buyer> builder)
    {
        builder.ToTable("Buyers");

        base.Configure(builder);

        builder.Property(a => a.Id).UseIdentityColumn();
        builder.Property(a => a.Description);
    }
}