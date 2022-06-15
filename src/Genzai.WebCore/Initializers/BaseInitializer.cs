using Genzai.WebCore.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Genzai.WebCore.Initializers;

/// <summary>
/// Base intializer
/// </summary>
public static class BaseInitializer
{

    /// <summary>
    /// It sets service provider
    /// </summary>
    /// <param name="app">App</param>
    /// <param name="env">Enviroment</param>
    public static void Init(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //Establece el service provider
        ServiceProviderContext.ServiceProvider = app.ApplicationServices;
    }
}
