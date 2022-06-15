using Genzai.WebCore.Interfaces;
using Genzai.WebCore.Services;
using Genzai.WebCore.Test.Mock.Application.AutoMapper;
using Genzai.WebCore.Test.Mock.Application.Response;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Xunit;

namespace Genzai.WebCore.Test.Service;

public class CacheServiceTest
{
    private readonly ICacheService _cacheService;

    private IDictionary<RedisKey, Task<RedisValue>> cacheMap = new Dictionary<RedisKey, Task<RedisValue>>();


    public CacheServiceTest()
    {
        var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new SampleMapping()));
        Mock<IDatabase> databaseMock = new Mock<IDatabase>();

        SampleResponse response = new SampleResponse()
        {
            Id = 1,
            Name = "Test"
        };

        Task<RedisValue> expectedValue = Task.FromResult<RedisValue>(JsonConvert.SerializeObject(response));
        cacheMap.Add("Test", expectedValue);
        databaseMock.Setup(database => database.StringGetAsync(It.IsAny<RedisKey>(), CommandFlags.None))
            .Returns((RedisKey redisKey, CommandFlags flags) =>
            {
                return cacheMap[redisKey];
            });

        databaseMock.Setup(database => database.KeyDeleteAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
            .Returns((RedisKey redisKey, CommandFlags flags) =>
            {
                cacheMap.Remove(redisKey);
                return Task.FromResult<bool>(true);
            });

        databaseMock.Setup(database => database.KeyDeleteAsync(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
            .Returns((RedisKey redisKey, CommandFlags flags) =>
            {
                cacheMap.Remove(redisKey);
                return Task.FromResult<bool>(true);
            });

        databaseMock.Setup(database => database.KeyExists(It.IsAny<RedisKey>(), It.IsAny<CommandFlags>()))
            .Returns((RedisKey redisKey, CommandFlags flags) =>
            {
                return cacheMap.ContainsKey(redisKey);
            });



        Mock<IConnectionMultiplexer> connectionMultiplexer = new Mock<IConnectionMultiplexer>();
        connectionMultiplexer.Setup(cache => cache.GetDatabase(-1, null)).Returns(databaseMock.Object);

        var mockLogger = new Mock<ILogger<CacheService>>();
        Mock<ILoggerFactory> mockLoggerFactory = new Mock<ILoggerFactory>();
        mockLoggerFactory.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(() => mockLogger.Object);
        _cacheService = new CacheService(connectionMultiplexer.Object, mockLoggerFactory.Object, mapperConfiguration.CreateMapper());

    }


    [Fact]
    public void TestGetFromCache()
    {
        SampleResponse value = _cacheService.GetFromCache<SampleResponse>("TestIncorrect");
        Assert.Null(value);
        value = _cacheService.GetFromCache<SampleResponse>("Test");
        Assert.Equal(1L, value.Id);

    }


    [Fact]
    public void TestFlushCacheByKey()
    {
        SampleResponse value = _cacheService.GetFromCache<SampleResponse>("Test");
        Assert.Equal(1L, value.Id);

        _cacheService.FlushCacheByKey("Test");


        value = _cacheService.GetFromCache<SampleResponse>("Test");
        Assert.Null(value);

    }
}
