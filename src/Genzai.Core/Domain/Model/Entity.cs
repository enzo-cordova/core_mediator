namespace Genzai.Core.Domain.Model;

/// <summary>
/// Base Entity.
/// </summary>
/// <typeparam name="TEntity">Entity Type.</typeparam>
/// <typeparam name="TKey">Key Type.</typeparam>
[ExcludeFromCodeCoverage]
public abstract class Entity<TEntity, TKey> : IEquatable<TEntity>, IEntity<TKey>
        where TEntity : Entity<TEntity, TKey>
        where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets or sets entity Key.
    /// </summary>
    public virtual TKey Id
    {
        get;
        protected set;
    }

    /// <summary>
    /// Operator == to compare entities.
    /// </summary>
    /// <param name="left">left entity.</param>
    /// <param name="right">right entity.</param>
    /// <returns>true or false.</returns>
    public static bool operator ==(Entity<TEntity, TKey> left, Entity<TEntity, TKey> right)
    {
        return EqualityComparer<Entity<TEntity, TKey>>.Default.Equals(left, right);
    }

    /// <summary>
    /// Operator != to compare entities.
    /// </summary>
    /// <param name="left">left entity.</param>
    /// <param name="right">right entity.</param>
    /// <returns>true or false.</returns>
    public static bool operator !=(Entity<TEntity, TKey> left, Entity<TEntity, TKey> right)
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
        var compareTo = obj as Entity<TEntity, TKey>;

        if (object.ReferenceEquals(this, compareTo))
        {
            return true;
        }

        if (compareTo == null || !(compareTo is TEntity))
        {
            return false;
        }

        if (this.IsTransient())
        {
            return false;
        }

        return this.HasSameNonDefaultIdAs(compareTo);
    }

    /// <summary>
    /// Method for returning Hash code.
    /// </summary>
    /// <returns>Hash code int.</returns>
    public override int GetHashCode()
    {
        const int hashMultiplier = 31;

        if (this.IsTransient())
        {
            return HashCode.Combine(this.GetType(), this.Id);
        }
        else
        {
            unchecked
            {
                // It's possible for two objects to return the same hash code based on
                // identically valued properties, even if they're of two different types,
                // so we include the object's type in the hash calculation
                int hashCode = HashCode.Combine(this.GetType());

                return (hashCode * hashMultiplier) ^ HashCode.Combine(this.Id);
            }
        }
    }

    /// <summary>
    /// Returns true if self and provided class has the same non default id.
    /// </summary>
    /// <param name="compareTo">provided class.</param>
    /// <returns>true or false.</returns>
    private bool HasSameNonDefaultIdAs(Entity<TEntity, TKey> compareTo)
    {
        return !this.IsTransient() && !compareTo.IsTransient() && this.Id.Equals(compareTo.Id);
    }

    /// <summary>
    /// Check if its not associated yet in database id=0.
    /// </summary>
    /// <returns>true or false.</returns>
    public virtual bool IsTransient() => this.Id.Equals(default);
}