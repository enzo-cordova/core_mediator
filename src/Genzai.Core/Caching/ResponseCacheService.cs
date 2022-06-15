using Microsoft.Extensions.Caching.Distributed;

namespace Genzai.Core.Caching
{
    /// <summary>
    /// Service for Redis cache. Maybe better in Core since it is used in several projects
    /// </summary>
    public class ResponseCacheService : IResponseCacheService
    {
        private readonly IDistributedCache _distributedCache;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="distributedCache"></param>
        public ResponseCacheService(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        /// <summary>
        /// Cached Response values
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <param name="response"></param>
        /// <param name="timeToLive"></param>
        public async Task CachedResponseAsync(string cacheKey, object response, TimeSpan timeToLive)
        {
            if (response == null)
                return;

            var serializeResponse = JsonConvert.SerializeObject(response);

            await _distributedCache.SetStringAsync(cacheKey, serializeResponse, new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = timeToLive
            });
        }

        /// <summary>
        /// Get CachedResponses Values
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public async Task<T> GetCachedResponseAsync<T>(string cacheKey)
        {
            var value = await _distributedCache.GetStringAsync(cacheKey);
            return value == null ? default : JsonConvert.DeserializeObject<T>(value);
        }

        /// <summary>
        /// Get CachedResponses Values
        /// </summary>
        /// <param name="cacheKey"></param>
        /// <returns></returns>
        public async Task<string> GetCachedResponseAsync(string cacheKey)
        {
            return await _distributedCache.GetStringAsync(cacheKey);
        }

        /// <summary>
        /// Remove key
        /// </summary>
        /// <param name="cacheKey"></param>
        public async Task RemoveCachedResponseAsync(string cacheKey)
        {
            await _distributedCache.RemoveAsync(cacheKey);
        }
    }
}