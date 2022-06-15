namespace Genzai.Core.Caching
{
    /// <summary>
    /// IResponseCacheService
    /// </summary>
    public interface IResponseCacheService
    {
        /// <summary>
        /// CachedResponseAsync
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="response"></param>
        /// <param name="timeToLive"></param>
        /// <returns></returns>
        Task CachedResponseAsync(string cacheKey, object response, TimeSpan timeToLive);

        /// <summary>
        /// GetCachedResponseAsync
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        Task<string> GetCachedResponseAsync(string cacheKey);

        /// <summary>
        /// GetCachedResponseAsync
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        Task<T> GetCachedResponseAsync<T>(string cacheKey);

        /// <summary>
        /// RemoveCachedResponseAsync
        /// </summary>
        /// <param name="cacheKey"></param>
        Task RemoveCachedResponseAsync(string cacheKey);
    }
}