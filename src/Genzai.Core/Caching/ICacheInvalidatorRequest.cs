namespace Genzai.Core.Caching;

/// <summary>
/// ICacheInvalidatorRequest
/// </summary>
public interface ICacheInvalidatorRequest
{
    /// <summary>
    /// CacheKey
    /// </summary>
    string CacheKey { get; }
}