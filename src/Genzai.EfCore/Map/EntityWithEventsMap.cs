namespace Genzai.EfCore.Map;

/// <summary>
/// Entity with events map.
/// </summary>
/// <typeparam name="TEntity">Entity Type.</typeparam>
/// <typeparam name="TKey">Key Type.</typeparam>
public abstract class EntityWithEventsMap<TEntity, TKey> : EntityMap<TEntity, TKey>
    where TEntity : EntityWithEvents<TEntity, TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Configure map.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    public override void Configure(EntityTypeBuilder<TEntity> builder)
    {
        base.Configure(builder);

        builder.Ignore(x => x.DomainEvents);
    }
}