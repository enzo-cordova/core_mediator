using Genzai.WebCore.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Genzai.WebCore.Extensions;

/// <summary>
/// Swager extensions.
/// </summary>
public static class SwaggerExtensions
{
    /// <summary>
    /// Add swagger support.
    /// </summary>
    /// <param name="services">Service</param>
    /// <param name="currentAssembly">Assembly</param>
    /// <param name="swaggerTitle">Swagger title</param>
    /// <param name="apiVersion">Api version</param>
    public static void AddSwaggerSupport(this IServiceCollection services,
        Assembly currentAssembly, string swaggerTitle, string apiVersion)
    {

        //Swagger
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc(apiVersion, new OpenApiInfo { Title = swaggerTitle, Version = apiVersion });
            c.CustomSchemaIds((type) => SchemaNameUtil.GetSchemaName(type));
            c.OperationFilter<FromQueryModelFilter>();
            c.OperationFilter<SwaggerResponsesOperationFilter>();
            c.OperationFilter<PatchBuilderSchema>();
            c.SchemaFilter<SwaggerSchemaAttribute>();
            c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

            string[] xmlDocs = currentAssembly.GetReferencedAssemblies()
            .Union(new AssemblyName[] { currentAssembly.GetName() })
            .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
            .Where(f => File.Exists(f)).ToArray();
            Array.ForEach(xmlDocs, (d) =>
            {
                c.IncludeXmlComments(d);
            });
        });
    }
    /// <summary>
    /// Configure swagger.
    /// </summary>
    /// <param name="app">Application builder.</param>
    /// <param name="provider">Provider</param>
    /// <param name="basePath">Base path</param>
    public static void ConfigureSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, string basePath)
    {

        app.UseSwagger(config =>
        {
            config.RouteTemplate = basePath + "/swagger/{documentName}/swagger.json";
        });
        app.UsePathBase($"/{basePath}");
        app.UseSwaggerUI(config =>
        {
            foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
            {
                config.SwaggerEndpoint($"/{basePath}/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
            }
        });
    }

}
