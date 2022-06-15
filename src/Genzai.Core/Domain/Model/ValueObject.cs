namespace Genzai.Core.Domain.Model;

/// <summary>
/// Standard base class for comparison values.
/// </summary>
/// <typeparam name="T">type of class.</typeparam>
[ExcludeFromCodeCoverage]
public abstract class ValueObject<T> : IEquatable<T>
    where T : ValueObject<T>
{
    /// <summary>
    /// For Hash codes uniqueness.
    /// </summary>
    private const int HasMultiplier = 31;

    /// <summary>
    /// Operator == to compare entities.
    /// </summary>
    /// <param name="left">left entity.</param>
    /// <param name="right">right entity.</param>
    /// <returns>true or false.</returns>
    public static bool operator ==(ValueObject<T> left, ValueObject<T> right)
    {
        return Equals(left, right);
    }

    /// <summary>
    /// Operator != to compare entities.
    /// </summary>
    /// <param name="left">left entity.</param>
    /// <param name="right">right entity.</param>
    /// <returns>true or false.</returns>
    [ExcludeFromCodeCoverage]
    public static bool operator !=(ValueObject<T> left, ValueObject<T> right)
    {
        return !Equals(left, right);
    }

    /// <summary>
    /// Current object is equal to other.
    /// </summary>
    /// <param name="other">other object to be compared.</param>
    /// <returns>true or false.</returns>
    public virtual bool Equals(T other)
    {
        if (other == null || other.GetType() != this.GetType())
        {
            return false;
        }

        // Now check properties and values
        IEnumerator<object> thisValues = this.GetAtomicValues().GetEnumerator();
        IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();

        while (thisValues.MoveNext() && otherValues.MoveNext())
        {
            if (thisValues.Current is null ^ otherValues.Current is null)
            {
                return false;
            }

            if (thisValues.Current?.Equals(otherValues.Current) == false)
            {
                return false;
            }
        }

        return !thisValues.MoveNext() && !otherValues.MoveNext();
    }

    /// <summary>
    /// Current object is equal to other.
    /// </summary>
    /// <param name="obj">object to be compared.</param>
    /// <returns>true or false.</returns>
    [ExcludeFromCodeCoverage]
    public override bool Equals(object obj)
    {
        if (!(obj is T other))
        {
            return false;
        }

        return this.Equals(other);
    }

    /// <summary>
    /// Method for returning Hash code.
    /// </summary>
    /// <returns>Hash code int.</returns>
    [ExcludeFromCodeCoverage]
    public override int GetHashCode()
    {
        int hashCode = this.GetType().GetHashCode();
        IEnumerable<object> thisValues = this.GetAtomicValues();

        thisValues.ToList().ForEach(element =>
        {
            if (element != null)
            {
                unchecked
                {
                    hashCode = (hashCode * HasMultiplier) + element.GetHashCode();
                }
            }
        });

        return hashCode;
    }

    /// <summary>
    /// Get values of object.
    /// </summary>
    /// <returns>Colection of values.</returns>
    protected abstract IEnumerable<object> GetAtomicValues();
}