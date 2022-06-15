using AutoMapper;
using Genzai.WebCore.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Genzai.WebCore.Services;

/// <summary>
/// It manages cache
/// </summary>
public class CacheService : ICacheService
{
    //Tiempo de expiracion de la cache por defecto
    private const long DefaultCacheExpirationSeconds = 7200;
    private const long EntityNotFoundId = -1;
    private const string ErrorRedis = "Redis error";

    /// <summary>
    /// Cache
    /// </summary>
    private readonly IConnectionMultiplexer _cache;

    /// <summary>
    /// Mapper
    /// </summary>
    private readonly IMapper _mapper;
    /// <summary>
    /// Logger
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="cache">Cache</param>
    /// <param name="loggerFactory">Logger Factory</param>
    /// <param name="mapper">Mapper</param>
    public CacheService(IConnectionMultiplexer cache, ILoggerFactory loggerFactory, IMapper mapper)
    {
        _cache = cache;
        _logger = loggerFactory.CreateLogger(GetType().ToString());
        _mapper = mapper;
    }

    /// <inheritdoc/>
    public T GetFromCache<T>(string cacheKey)
    {
        try
        {
            IDatabase database = GetCacheDatabase();
            if (database != null)
            {
                RedisValue cacheResult = database.StringGetAsync(cacheKey).Result;
                string resultStr = cacheResult.ToString();
                if (resultStr != null)
                {
                    return JsonConvert.DeserializeObject<T>(resultStr);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorRedis);
        }

        return default;
    }


    /// <inheritdoc/>
    public void PutOnCache<T>(string cacheKey, T cacheValue, long expirationSeconds)
    {
        try
        {
            IDatabase database = GetCacheDatabase();
            if (database != null)
            {
                string cachedString = JsonConvert.SerializeObject(cacheValue, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                database.StringSetAsync(cacheKey, cachedString, TimeSpan.FromSeconds(expirationSeconds));
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorRedis);
        }
    }

    /// <inheritdoc/>
    public void FlushCacheByKey(string cacheKey)
    {
        try
        {
            if (KeyExists(cacheKey))
            {
                IDatabase database = GetCacheDatabase();
                if (database != null)
                {
                    database.KeyDeleteAsync(cacheKey);
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorRedis);
        }
    }

    /// <inheritdoc/>
    public void FlushCacheByPattern(string cacheKeyPattern)
    {
        foreach (string cacheKey in GetKeysByPattern(cacheKeyPattern + "*"))
        {
            FlushCacheByKey(cacheKey);
        }
    }

    /// <inheritdoc/>
    public IEnumerable<string> GetKeysByPattern(string pattern)
    {
        return GetKeysAsync(pattern).Result;
    }

    private async Task<IEnumerable<string>> GetKeysAsync(string pattern)
    {
        IList<string> returnObject = new List<string>();
        try
        {
            if (_cache != null)
            {
                foreach (System.Net.EndPoint endpoint in _cache.GetEndPoints())
                {
                    IServer server = _cache.GetServer(endpoint);
                    if (server != null)
                    {
                        IAsyncEnumerable<RedisKey> keys = server.KeysAsync(pattern: pattern);

                        List<RedisKey> list = await keys.ToListAsync();
                        foreach (RedisKey key in list)
                        {
                            returnObject.Add(key.ToString());
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorRedis);
        }
        return returnObject;
    }
    /// <inheritdoc/>
    public bool KeyExists(string cacheKey)
    {
        try
        {
            IDatabase database = GetCacheDatabase();
            return database != null && database.KeyExists(cacheKey);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorRedis);
        }
        return false;
    }

    /// <inheritdoc/>
    public void FlushAll()
    {
        try
        {
            System.Net.EndPoint[] endpoints = _cache.GetEndPoints(true);
            foreach (System.Net.EndPoint endpoint in endpoints)
            {
                IServer server = _cache.GetServer(endpoint);
                server.FlushAllDatabases();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ErrorRedis);
        }
    }

    /// <summary>
    /// It returns database
    /// </summary>
    /// <returns>Database cache</returns>
    protected IDatabase GetCacheDatabase()
    {
        return _cache != null ? _cache.GetDatabase() : null;
    }


    /// <summary>
    /// It returns default expiration in seconds
    /// </summary>
    /// <returns>Default expiration in seconds</returns>
    public virtual long GetDefaultExpirationSeconds()
    {
        return DefaultCacheExpirationSeconds;
    }

    /// <summary>
    /// It completes not found entities
    /// </summary>
    /// <param name="entityIdBySubEntityId">Entity By subentity Id</param>
    /// <param name="noCachedEntityIds">No caches ids</param>
    public virtual void CompleteNotFoundEntities(Dictionary<long, long> entityIdBySubEntityId, IList<long> noCachedEntityIds)
    {
        foreach (long noCachedEntityId in noCachedEntityIds)
        {
            if (!entityIdBySubEntityId.ContainsKey(noCachedEntityId))
            {
                entityIdBySubEntityId.Add(noCachedEntityId, EntityNotFoundId);
            }
        }
    }

    /// <inheritdoc/>
    public async Task<TMapper> SearchAndFillIfNotExits<T, TProperty, TMapper>(String cacheKey, Func<TProperty, ValueTask<T>> predicate, TProperty property)
    {
        TMapper result = GetFromCache<TMapper>(cacheKey);
        if (result != null)
        {
            return result;
        }

        var resultFunction = await predicate(property);
        result = _mapper.Map<TMapper>(resultFunction);
        PutOnCache(cacheKey, _mapper.Map<TMapper>(resultFunction), GetDefaultExpirationSeconds());
        return result;

    }

    /// <inheritdoc/>
    public TMapper SearchAndFillIfNotExits<T, TProperty, TMapper>(String cacheKey, Func<TProperty, T> predicate, TProperty property)
    {
        TMapper result = GetFromCache<TMapper>(cacheKey);
        if (result != null)
        {
            return result;
        }
        var resultFunction = predicate(property);
        result = _mapper.Map<TMapper>(resultFunction);
        PutOnCache(cacheKey, _mapper.Map<TMapper>(resultFunction), GetDefaultExpirationSeconds());
        return result;
    }



    /// <inheritdoc/>
    public TMapper SearchAndFillIfNotExits<T, TMapper>(String cacheKey, Func<T> predicate)
    {
        TMapper result = GetFromCache<TMapper>(cacheKey);
        if (result != null)
        {
            return result;
        }


        var resultFunction = predicate();
        result = _mapper.Map<TMapper>(resultFunction);
        PutOnCache(cacheKey, _mapper.Map<TMapper>(resultFunction), GetDefaultExpirationSeconds());
        return result;
    }



}
