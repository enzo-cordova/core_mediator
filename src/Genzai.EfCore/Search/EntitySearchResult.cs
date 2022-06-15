namespace Genzai.EfCore.Search;

/// <summary>
/// Entity search result
/// </summary>
/// <typeparam name="TKey"></typeparam>
public class EntitySearchResult<TKey> where TKey : IEquatable<TKey>
{
    /// <summary>
    /// Key
    /// </summary>
    public TKey? Id { get; set; }
}
