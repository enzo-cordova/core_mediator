namespace Genzai.EfCore.Tests.Mock.Mappings;

/// <summary>
/// Auditable map
/// </summary>
public class AuditableOrderMap : AuditableEntityMap<AuditableOrder, int>
{
    /// <summary>
    /// Configure method.
    /// </summary>
    /// <param name="builder">Builder param.</param>
    public override void Configure(EntityTypeBuilder<AuditableOrder> builder)
    {
        builder.ToTable("AuditableOrders");

        base.Configure(builder);

        builder.Property(a => a.Id).UseIdentityColumn();
        builder.Property(o => o.Description);
        builder.OwnsOne(o => o.Address, a => a.WithOwner());
    }
}