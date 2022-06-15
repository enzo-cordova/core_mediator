using Genzai.EfCore.Search;

namespace Genzai.EfCore.Repository;

/// <summary>
/// It controls partial searches
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
/// <typeparam name="TKey">Enity key</typeparam>
/// <typeparam name="TEntitySearch">Entity search</typeparam>
/// <typeparam name="TEntitySearchResult">Entity search result</typeparam>
public interface IPartialSearchRepository<TEntity, TKey, TEntitySearch, TEntitySearchResult> : IRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TEntitySearch : EntitySearch
    where TEntitySearchResult : EntityIdLongSearchResult
{
    /// <summary>
    /// It performs searches by querying the database
    /// </summary>
    /// <param name="search">Search</param>
    /// <returns>Search results</returns>
    Task<List<TEntitySearchResult>> Search(TEntitySearch search);

    /// <summary>
    /// It performs searches by querying the database
    /// </summary>
    /// <param name="search">Search</param>
    /// <returns>Search count</returns>
    Task<int> SearchSize(TEntitySearch search);
}
