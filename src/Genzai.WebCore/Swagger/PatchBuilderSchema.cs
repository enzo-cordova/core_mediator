using Genzai.WebCore.Constants;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Genzai.WebCore.Swagger;

/// <summary>
/// Class for build patch schema
/// </summary>
public class PatchBuilderSchema : IOperationFilter
{
    /// <summary>
    /// Create patch schema if operation is patch
    /// </summary>
    /// <param name="operation">Operation</param>
    /// <param name="context">Context</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        //Si la operación es un patch, crea un nuevo esquema para el patch sin required
        if (HttpMethod.Patch.ToString().Equals(context.ApiDescription.HttpMethod))
        {

            var content = operation.RequestBody.Content;
            var contentInside = content[CommonsConstants.AplicationJson];
            OpenApiSchema updateSchema = CloneOpenApiSchema(context.SchemaRepository.Schemas[contentInside.Schema.Reference.Id]);

            OpenApiSchema newSchema = context.SchemaRepository.AddDefinition(string.Format("{0}Patch", contentInside.Schema.Reference.Id), updateSchema);
            OpenApiSchema schemaReference = context.SchemaRepository.Schemas[newSchema.Reference.Id];
            if (schemaReference.Required != null)
            {
                schemaReference.Required.Clear();
            }

            operation.RequestBody.Content[CommonsConstants.AplicationJson].Schema = newSchema;
        }
    }

    private OpenApiSchema CloneOpenApiSchema(OpenApiSchema clone)
    {
        OpenApiSchema openApiSchema = new OpenApiSchema();
        openApiSchema.Reference = clone.Reference;
        openApiSchema.Description = clone.Description;
        openApiSchema.Type = clone.Type;
        openApiSchema.Nullable = clone.Nullable;
        openApiSchema.Title = clone.Title;
        openApiSchema.Format = clone.Format;
        openApiSchema.Properties = new Dictionary<string, OpenApiSchema>();

        foreach (var property in clone.Properties)
        {
            OpenApiSchema innerProperty = CloneOpenApiSchema(property.Value);
            openApiSchema.Properties.Add(property.Key, innerProperty);
        }
        if (clone.Items != null)
        {
            openApiSchema.Items = CloneOpenApiSchema(clone.Items);
        }

        return openApiSchema;

    }
}
