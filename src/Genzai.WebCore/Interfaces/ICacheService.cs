namespace Genzai.WebCore.Interfaces;

/// <summary>
/// Interface for cache operations
/// </summary>
public interface ICacheService
{

    /// <summary>
    /// Get from cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cacheKey"></param>
    /// <returns></returns>
    T GetFromCache<T>(string cacheKey);





    /// <summary>
    /// Put object in cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cacheKey"></param>
    /// <param name="cacheValue"></param>
    /// <param name="expirationSeconds"></param>
    void PutOnCache<T>(string cacheKey, T cacheValue, long expirationSeconds);


    /// <summary>
    /// Get default expiration seconds
    /// </summary>
    /// <returns></returns>
    long GetDefaultExpirationSeconds();


    /// <summary>
    /// Complets entities with info for not found
    /// </summary>
    /// <param name="entityIdBySubEntityId"></param>
    /// <param name="noCachedEntityIds"></param>
    void CompleteNotFoundEntities(Dictionary<long, long> entityIdBySubEntityId, IList<long> noCachedEntityIds);

    /// <summary>
    /// It flushes cache by key
    /// </summary>
    /// <param name="cacheKey">Cache key</param>
    void FlushCacheByKey(string cacheKey);

    /// <summary>
    /// It flushes cache by pattern
    /// </summary>
    /// <param name="cacheKeyPattern">Pattern cache</param>
    void FlushCacheByPattern(string cacheKeyPattern);

    /// <summary>
    /// It returns keys by pattern
    /// </summary>
    /// <param name="pattern">pattern</param>
    /// <returns> keys by pattern</returns>
    IEnumerable<string> GetKeysByPattern(string pattern);

    /// <summary>
    /// Check if exits key
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <returns></returns>
    bool KeyExists(string cacheKey);

    /// <summary>
    /// Flush all cache
    /// </summary>
    void FlushAll();


    /// <summary>
    /// Search object in cache, if not exitis, execute function and put 
    /// result in cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <typeparam name="TMapper"></typeparam>
    /// <param name="cacheKey"></param>
    /// <param name="predicate"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    Task<TMapper> SearchAndFillIfNotExits<T, TProperty, TMapper>(String cacheKey, Func<TProperty, ValueTask<T>> predicate, TProperty property);


    /// <summary>
    /// Search object in cache, if not exitis, execute function and put 
    /// result in cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TProperty"></typeparam>
    /// <typeparam name="TMapper"></typeparam>
    /// <param name="cacheKey"></param>
    /// <param name="predicate"></param>
    /// <param name="property"></param>
    /// <returns></returns>
    TMapper SearchAndFillIfNotExits<T, TProperty, TMapper>(String cacheKey, Func<TProperty, T> predicate, TProperty property);


    /// <summary>
    /// Search object in cache, if not exitis, execute function and put 
    /// result in cache
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TMapper"></typeparam>
    /// <param name="cacheKey"></param>
    /// <param name="predicate"></param>
    /// <returns></returns>
    TMapper SearchAndFillIfNotExits<T, TMapper>(String cacheKey, Func<T> predicate);
}
