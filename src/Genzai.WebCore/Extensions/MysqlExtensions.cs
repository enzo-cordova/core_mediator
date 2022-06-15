using Genzai.Core.Model.Configuration.Mapping;
using Genzai.WebCore.Vault;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Genzai.WebCore.Extensions;

/// <summary>
/// Mysql extensions.
/// </summary>
public static class MysqlExtensions
{
    /// <summary>
    /// Add mysql support.
    /// </summary>
    /// <param name="services">Service Container.</param>
    /// <param name="configuration">Configuration</param>
    public static void AddMysqlSupport<TContext>(this IServiceCollection services,
        IConfiguration configuration)
        where TContext : DbContext
    {
        AddMysqlSupport<TContext>(services, configuration, null);
    }

    /// <summary>
    /// Add mysql support.
    /// </summary>
    /// <param name="services">Service Container.</param>
    /// <param name="configuration">Configuration</param>
    /// <param name="migrationAssembly">Migration assembly</param>
    public static void AddMysqlSupport<TContext>(this IServiceCollection services, IConfiguration configuration,
        string migrationAssembly)
        where TContext : DbContext
    {
        VaultConfigurationLoader.Load(configuration);

        //Database connection
        var connectionConfig = configuration.GetSection(SqlConnectionConfiguration.Section).Get<SqlConnectionConfiguration>();
        if (migrationAssembly != null)
        {
            services.AddDbContext<TContext>(options =>
                options.UseLazyLoadingProxies().UseMySql(connectionConfig.AppConnection,
                ServerVersion.AutoDetect(connectionConfig.AppConnection),
                    x => x.MigrationsAssembly(migrationAssembly)));
        }
        else
        {
            services.AddDbContext<TContext>(options =>
             options.UseLazyLoadingProxies().UseMySql(connectionConfig.AppConnection,
             ServerVersion.AutoDetect(connectionConfig.AppConnection)));
        }
    }
}
