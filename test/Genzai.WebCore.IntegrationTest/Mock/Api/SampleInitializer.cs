using Genzai.WebCore.Initializers;
using Genzai.WebCore.Test.Mock.Infrastructure.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Genzai.WebCore.Integration.Test.Mock.Api
{

    /// <summary>
    /// Initializer
    /// </summary>
    public static class SampleInitializer
    {
        /// <summary>
        /// It executes actions on init
        /// </summary>
        /// <param name="app">App</param>
        /// <param name="env">Enviroment</param>
        /// <param name="configuration">Configuration</param>
        public static void Init(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            //Service context
            BaseInitializer.Init(app, env);
            //Database init
            IServiceScope serviceScope = app.ApplicationServices.CreateScope();
            SampleContext context = serviceScope.ServiceProvider.GetService<SampleContext>();
            DatabaseInitializer.Init(app, configuration, context);
        }
    }
}

