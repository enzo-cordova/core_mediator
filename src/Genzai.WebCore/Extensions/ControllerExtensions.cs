using Genzai.WebCore.RequestFilters;
using Microsoft.Extensions.DependencyInjection;

namespace Genzai.WebCore.Extensions;

/// <summary>
/// Controller extensions.
/// </summary>

public static class ControllerExtensions
{
    /// <summary>
    /// Add swagger support.
    /// </summary>
    /// <param name="services">Service Container.</param>
    public static void AddControllersSupport(this IServiceCollection services)
    {

        //Controller
        services.AddControllers(options =>
        {
            options.Filters.Add(typeof(HttpResponseExceptionFilter));
            options.Filters.Add(typeof(CorrelationIdFilter));
        });
    }
}
