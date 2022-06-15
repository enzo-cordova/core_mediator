using Genzai.Core.Model.Configuration.Mapping;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace Genzai.WebCore.Extensions;

/// <summary>
/// Redis extensions.
/// </summary>
public static class RedisExtensions
{
    /// <summary>
    /// Add redis support.
    /// </summary>
    /// <param name="services">Services</param>
    /// <param name="configuration">Configuration</param>
    public static void AddRedisSupport(this IServiceCollection services, IConfiguration configuration)
    {

        //Redis
        var connectionConfig = configuration.GetSection(SqlConnectionConfiguration.Section).Get<SqlConnectionConfiguration>();
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(connectionConfig.Redis));

    }


}
