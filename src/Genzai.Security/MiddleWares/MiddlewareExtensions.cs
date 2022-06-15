using Genzai.Core.Caching;
using Genzai.Core.Model.Configuration.Mapping;
using Genzai.Security.Context;
using Genzai.Security.Domain;
using Genzai.Security.Domain.Interfaces; 
using Genzai.Security.Repository;
using Genzai.Security.Services.Implementations;
using Genzai.Security.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Genzai.Core.Telemetry;

namespace Genzai.Security.MiddleWares;

/// <summary>
/// MiddlewareExtensions
/// </summary>
[ExcludeFromCodeCoverage]
public static class MiddlewareExtensions
{
    /// <summary>
    /// Pipeline helper
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseGLogin(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GLoginMiddleware>();
    }

    /// <summary>
    /// Add EF context database.
    /// </summary>
    /// <param name="services">Service Container.</param>
    /// <param name="config">Configuration root.</param>
    public static void AddApplicationContextSecurity(this IServiceCollection services, ConfigurationManager config)
    {
        var connectionConfig = config.GetSection(SqlConnectionConfiguration.Section).Get<SqlConnectionConfiguration>();

        services.AddDbContext<AuthorizationContext>(options =>
           options.UseLazyLoadingProxies().UseMySql(connectionConfig.AppConnection, ServerVersion.AutoDetect(connectionConfig.AppConnection),
               context => { context.EnableStringComparisonTranslations(); }));
        services.AddScoped<ITelemetryProvider, TelemetryProvider>(); 
        services.AddStackExchangeRedisCache(options => options.Configuration = connectionConfig.Redis);
    }

    /// <summary>
    /// Services registration helper
    /// </summary>
    /// <param name="serviceCollection"></param>
    /// <returns></returns>
    public static IServiceCollection AddGLogin(this IServiceCollection serviceCollection)
    {
        return serviceCollection 
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped(typeof(IAutoEnrollmentService<User>), typeof(AutoEnrollmentService))
             .AddScoped<IPermissionRepository, PermissionRepository>()
               .AddScoped<IResponseCacheService, ResponseCacheService>()
            .AddScoped<IPermissionsService, PermissionsService>();
    }
}