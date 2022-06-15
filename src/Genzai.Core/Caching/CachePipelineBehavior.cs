namespace Genzai.Core.Caching;

/// <summary>
/// CachePipelineBehavior
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class CachePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IResponseCacheService _cache;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="cache"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public CachePipelineBehavior(IResponseCacheService cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    /// <summary>
    /// Handle
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        if (request is ICacheableRequest<TResponse> cacheableRequest)
        {
            var key = cacheableRequest.CacheKey;
            var cachedResponse = await _cache.GetCachedResponseAsync(key);
            if (cachedResponse != null)
                return JsonConvert.DeserializeObject<TResponse>(cachedResponse);
            var respon = await next();
            await _cache.CachedResponseAsync(key, respon, TimeSpan.FromMinutes(20));
            return respon;
        }
        var response = await next();
        if (request is ICacheInvalidatorRequest cacheInvalidationRequest)
            await _cache.RemoveCachedResponseAsync(cacheInvalidationRequest.CacheKey);
        return response;
    }
}