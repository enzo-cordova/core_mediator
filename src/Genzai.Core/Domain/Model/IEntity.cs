namespace Genzai.Core.Domain.Model;

/// <summary>
/// Entity contract.
/// </summary>
/// <typeparam name="TKey">Type of key.</typeparam>
public interface IEntity<TKey>
    where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Gets entity Key.
    /// </summary>
    TKey Id { get; }
}