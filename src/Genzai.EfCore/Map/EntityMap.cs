namespace Genzai.EfCore.Map;

/// <summary>
/// Entity base map.
/// </summary>
/// <typeparam name="TEntity">Entity.</typeparam>
/// <typeparam name="TKey">Entity Key.</typeparam>
public abstract class EntityMap<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity<TEntity, TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Configure map.
    /// </summary>
    /// <param name="builder">Entity builder.</param>
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        if (builder == null)
        {
            Guard.Against<ArgumentNullException>(
                true,
                string.Format(CultureInfo.InvariantCulture, LocalStrings.ParameterIsNull, nameof(builder)));
        }
        else
        {
            builder.HasKey(entity => entity.Id);
        }
    }
}