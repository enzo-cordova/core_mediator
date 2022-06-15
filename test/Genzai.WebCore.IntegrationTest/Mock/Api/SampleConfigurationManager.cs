using Genzai.WebCore.Extensions;
using Genzai.WebCore.Integration.Test.Mock.Api.Extensions;
using Genzai.WebCore.Logging;
using Genzai.WebCore.Test.Mock.Infrastructure.Data.Context;
using Genzai.WebCore.Vault;
using System.Reflection;

namespace Genzai.WebCore.Integration.Test.Mock.Api
{

    /// <summary>
    /// Configuration manager
    /// </summary>
    public static class SampleConfigurationManager
    {

        /// <summary>
        /// It configures Sample services
        /// </summary>
        /// <param name="services">Services</param>
        /// <param name="configuration">Configuration</param>
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //vault
            VaultConfigurationLoader.Load(configuration);
            //
            services.AddSwaggerSupport(Assembly.GetExecutingAssembly(), "SamplePortalAPI", "v1");
            //Controllers
            services.AddControllersSupport();
            //Validations
            services.AddApplicationValidations();
            //Healt check
            services.AddHealthChecks();
            //Identity
            services.AddScoped(provider => new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new("emails", "mock@email.fake"),
                new(ClaimTypes.Name, "anynameazuread@prosegur.com")
            }, "mock")));

            //Redis
            services.AddRedisSupport(configuration);
            //Log
            services.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            services.AddApplicationInsightsTelemetry();
            //Api version
            services.AddApiVersioningSupport(1, 0);
            //Repositories
            services.AddApplicationRepositories();

            //Mediatr and mapper
            services.RegisterServices();
            //Mysql
            services.AddMysqlSupport<SampleContext>(configuration, "Gedge.Infrastructure");
        }


    }
}
