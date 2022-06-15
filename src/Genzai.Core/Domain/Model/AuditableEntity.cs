namespace Genzai.Core.Domain.Model;

/// <summary>
/// IAuditable MarkUp Interface
/// </summary>
public interface IAuditable
{
    /// <summary>
    /// Audit creation information
    /// </summary>
    /// <param name="userName"></param>
    void CreatedInformation(string userName);

    /// <summary>
    /// Audit update information
    /// </summary>
    /// <param name="userName"></param>
    void UpdateInformation(string userName);
}

/// <summary>
/// Base Entity.
/// </summary>
/// <typeparam name="TEntity">Entity Type.</typeparam>
/// <typeparam name="TKey">Key Type.</typeparam>
[ExcludeFromCodeCoverage]
public abstract class AuditableEntity<TEntity, TKey> : IEquatable<TEntity>, IEntity<TKey>
        where TEntity : AuditableEntity<TEntity, TKey>, IAuditable
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
    /// DateTime Creation
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// DateTime Update
    /// </summary>
    public DateTime? UpdatedAt { get; private set; }

    /// <summary>
    /// How create it
    /// </summary>
    public string CreatedBy { get; private set; }

    /// <summary>
    /// How update it
    /// </summary>
    public string UpdatedBy { get; private set; }

    /// <summary>
    /// Operator == to compare entities.
    /// </summary>
    /// <param name="left">left entity.</param>
    /// <param name="right">right entity.</param>
    /// <returns>true or false.</returns>
    public static bool operator ==(AuditableEntity<TEntity, TKey> left, AuditableEntity<TEntity, TKey> right)
    {
        return EqualityComparer<AuditableEntity<TEntity, TKey>>.Default.Equals(left, right);
    }

    /// <summary>
    /// Operator != to compare entities.
    /// </summary>
    /// <param name="left">left entity.</param>
    /// <param name="right">right entity.</param>
    /// <returns>true or false.</returns>
    public static bool operator !=(AuditableEntity<TEntity, TKey> left, AuditableEntity<TEntity, TKey> right)
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
        var compareTo = obj as AuditableEntity<TEntity, TKey>;

        if (ReferenceEquals(this, compareTo))
        {
            return true;
        }

        if (compareTo == null || !(compareTo is TEntity))
        {
            return false;
        }

        if (IsTransient())
        {
            return false;
        }

        return HasSameNonDefaultIdAs(compareTo);
    }

    /// <summary>
    /// Method for returning Hash code.
    /// </summary>
    /// <returns>Hash code int.</returns>
    public override int GetHashCode()
    {
        const int hashMultiplier = 31;

        if (IsTransient())
        {
            return HashCode.Combine(GetType(), Id);
        }
        else
        {
            unchecked
            {
                // It's possible for two objects to return the same hash code based on
                // identically valued properties, even if they're of two different types,
                // so we include the object's type in the hash calculation
                int hashCode = HashCode.Combine(GetType());

                return (hashCode * hashMultiplier) ^ HashCode.Combine(Id);
            }
        }
    }

    /// <summary>
    /// Returns true if self and provided class has the same non default id.
    /// </summary>
    /// <param name="compareTo">provided class.</param>
    /// <returns>true or false.</returns>
    private bool HasSameNonDefaultIdAs(AuditableEntity<TEntity, TKey> compareTo)
    {
        return !IsTransient() && !compareTo.IsTransient() && Id.Equals(compareTo.Id);
    }

    /// <summary>
    /// Check if its not associated yet in database id=0.
    /// </summary>
    /// <returns>true or false.</returns>
    public virtual bool IsTransient() => Id.Equals(default);

    /// <summary>
    /// Audit creation information
    /// </summary>
    /// <param name="userName"></param>
    public virtual void CreatedInformation(string userName)
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = userName;
    }

    /// <summary>
    /// Audit update information
    /// </summary>
    /// <param name="userName"></param>
    public virtual void UpdateInformation(string userName)
    {
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = userName;
    }
}