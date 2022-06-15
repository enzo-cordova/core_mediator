using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Genzai.WebCore.Extensions;

/// <summary>
/// Apiversion extensions.
/// </summary>
public static class ApiVersionExtensions
{
    /// <summary>
    /// Add apiversion support.
    /// </summary>
    /// <param name="services">Service Container.</param>
    /// <param name="majorVersion">Major version</param>
    /// <param name="minorVersion">Minor version</param>
    public static void AddApiVersioningSupport(this IServiceCollection services, int majorVersion, int minorVersion)
    {
        services.AddApiVersioning(config =>
        {
            config.DefaultApiVersion = new ApiVersion(majorVersion, minorVersion);
            config.AssumeDefaultVersionWhenUnspecified = true;
            config.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
    }
}
