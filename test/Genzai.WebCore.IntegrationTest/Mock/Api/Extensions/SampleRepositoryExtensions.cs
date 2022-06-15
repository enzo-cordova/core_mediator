using FluentValidation.AspNetCore;
using Genzai.WebCore.Interfaces;
using Genzai.WebCore.Services;
using Genzai.WebCore.Test.Mock.Domain.Repositories;
using Genzai.WebCore.Test.Mock.Infrastructure.Data.Repositories;

namespace Genzai.WebCore.Integration.Test.Mock.Api.Extensions
{
    /// <summary>
    /// GwatchRepository Extensions.
    /// </summary>
    public static class SampleRepositoryExtensions
    {
        /// <summary>
        /// Add Application Repositories.
        /// </summary>
        /// <param name="services">Service Container.</param>
        public static void AddApplicationRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISampleRepository, SampleRepository>();


            services.AddScoped<ICacheService, CacheService>();
        }


        /// <summary>
        /// Add Application Validations.
        /// </summary>
        /// <param name="services">Service Container.</param>
        public static void AddApplicationValidations(this IServiceCollection services)
        {

            services.AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssembly(typeof(Program).Assembly)
            );
        }

    }
}
