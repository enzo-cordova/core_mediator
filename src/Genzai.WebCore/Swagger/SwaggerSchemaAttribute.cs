using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Genzai.WebCore.Swagger;

/// <summary>
/// Schema attribute
/// </summary>
public class SwaggerSchemaAttribute : ISchemaFilter
{
    /// <inheritdoc/>
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties == null || schema.Properties.Count == 0)
        {
            return;
        }

        var requiredButNullableProperties = schema
           .Properties
           .Where(x => x.Value.Nullable && (x.Value.Title == null || !x.Value.Title.Contains("NULL")))
           .ToList();

        foreach (var property in requiredButNullableProperties)
        {
            property.Value.Nullable = false;
        }
    }
}
