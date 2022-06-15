namespace Genzai.Core.Caching;

/// <summary>
/// ICacheableRequest
/// </summary>
public interface ICacheableRequest<TResponse>
{
    /// <summary>
    /// CacheKey
    /// </summary>
    string CacheKey { get; }
}