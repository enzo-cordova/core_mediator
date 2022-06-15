using Microsoft.Extensions.DependencyInjection;

namespace Genzai.WebCore.Extensions;

/// <summary>
/// Cors extension
/// </summary>
public static class CorsExtension
{

    public const string CorsPolicy = "CorsPolicy";
    /// <summary>
    /// Add redis support.
    /// </summary>
    /// <param name="services">Services</param>
    /// <param name="origins">Origins</param>
    public static void AddCorsConfiguration(this IServiceCollection services, string[] origins)
    {
        services.AddCors(o => o.AddPolicy(CorsPolicy, builder =>
        {
            builder
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins(origins);

        }));
    }
}
