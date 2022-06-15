using Genzai.WebCore.Test.Mock.Application.AutoMapper;
using Genzai.WebCore.Utils;
using MediatR;
using System.Reflection;

namespace Genzai.WebCore.Integration.Test.Mock.Api.Extensions
{
    /// <summary>
    /// Services Extensions.
    /// </summary>
    public static class ServicesExtensions
    {
        /// <summary>
        /// Register Services.
        /// </summary>
        /// <param name="services">Service collection.</param>
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddMediatR(ReflectionUtils.GetSolutionAssemblies());
            services.AddAutoMapper(typeof(SampleMapping).GetTypeInfo().Assembly);
        }

    }
}