using Genzai.Core.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Genzai.Core.Tests.Caching;

public class ResponseCacheServiceTest
{
    private readonly string _cacheKey;

    public ResponseCacheServiceTest()
    {
        _cacheKey = "esx000000@prosegur.com";
    }

    [Fact]
    public async Task ResponseCacheService_SetCache_Test()
    {
        var cacheSeconds = 10;
        var cacheKey = $"{nameof(ResponseCacheService_SetCache_Test)}__{_cacheKey}";
        var cacheVal = DateTime.Now.ToString("s");
        var memoryCache = new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions()));

        var cache = new ResponseCacheService(memoryCache);
        Assert.NotNull(cache);

        // Add to cache
        {
            await cache.CachedResponseAsync(cacheKey, cacheVal, DateTime.Now.AddSeconds(cacheSeconds).TimeOfDay);
        }
        // Get cache value successfully
        {
            var res = await cache.GetCachedResponseAsync(cacheKey);
            Assert.Equal(JsonConvert.SerializeObject(cacheVal), res);
        }
    }

    [Fact]
    public async Task ResponseCacheService_GetCache_Test()
    {
        var cacheSeconds = 10;
        var cacheKey = $"{nameof(ResponseCacheService_SetCache_Test)}__{_cacheKey}";
        var cacheVal = DateTime.Now.ToString("s");
        var memoryCache = new MemoryDistributedCache(Options.Create(new MemoryDistributedCacheOptions()));

        var cache = new ResponseCacheService(memoryCache);
        Assert.NotNull(cache);

        // Add to cache
        {
            await cache.CachedResponseAsync(cacheKey, cacheVal, DateTime.Now.AddSeconds(cacheSeconds).TimeOfDay);
        }
        // Get cache value successfully
        {
            var res = await cache.GetCachedResponseAsync(cacheKey);
            Assert.Equal(JsonConvert.SerializeObject(cacheVal), res);
        }
    }
}