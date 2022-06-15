namespace Genzai.Core.Domain.Model.KeyLess;

/// <summary>
/// Entity Base
/// </summary>
/// <typeparam name="TEntity">Entity Type.</typeparam>
[ExcludeFromCodeCoverage]
public abstract class EntityBase<TEntity> : IEquatable<TEntity>
    where TEntity : EntityBase<TEntity>
{
    /// <summary>
    /// Operator == to compare entities.
    /// </summary>
    /// <param name="left">left entity.</param>
    /// <param name="right">right entity.</param>
    /// <returns>true or false.</returns>
    public static bool operator ==(EntityBase<TEntity> left, EntityBase<TEntity> right)
    {
        return EqualityComparer<EntityBase<TEntity>>.Default.Equals(left, right);
    }

    /// <summary>
    /// Operator != to compare entities.
    /// </summary>
    /// <param name="left">left entity.</param>
    /// <param name="right">right entity.</param>
    /// <returns>true or false.</returns>
    public static bool operator !=(EntityBase<TEntity> left, EntityBase<TEntity> right)
    {
        return !(left == right);
    }

    /// <summary>
    /// Check if is equals to other entity with the same type.
    /// </summary>
    /// <param name="other">TEntity other class.</param>
    /// <returns>true or false.</returns>
    public virtual bool Equals(TEntity other)
    {
        return base.Equals(other);
    }

    /// <summary>
    /// Check if is equals to other entity with the same type.
    /// </summary>
    /// <param name="obj">Object class.</param>
    /// <returns>true or false.</returns>
    public override bool Equals(object obj)
    {
        var compareTo = obj as EntityBase<TEntity>;

        if (ReferenceEquals(this, compareTo))
        {
            return true;
        }

        if (compareTo == null || !(compareTo is TEntity))
        {
            return false;
        }

        return false;
    }

    /// <summary>
    /// GetHashCode
    /// </summary>
    /// <returns>Hash code</returns>
    public override int GetHashCode()
    {
        const int hashMultiplier = 31;

        var actual = this;

        unchecked
        {
            // It's possible for two objects to return the same hash code based on
            // identically valued properties, even if they're of two different types,
            // so we include the object's type in the hash calculation
            int hashCode = HashCode.Combine(actual.GetType());

            return (hashCode * hashMultiplier) ^ HashCode.Combine(17);
        }
    }
}