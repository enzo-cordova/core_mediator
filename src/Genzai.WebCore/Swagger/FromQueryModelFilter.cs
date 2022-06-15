using Genzai.WebCore.Requests;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Genzai.WebCore.Swagger;

/// <summary>
/// Query model filter
/// </summary>
public class FromQueryModelFilter : IOperationFilter
{
    /// <inheritdoc/>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var description = context.ApiDescription;

        List<ParameterDescriptor> baseSearchDtos = description.ActionDescriptor.Parameters
                .Where(p =>
                {
                    return typeof(IEntitySearchRequest).IsAssignableFrom(p.ParameterType);
                })
                .ToList();
        if (baseSearchDtos != null)
        {
            foreach (var baseSearchDto in baseSearchDtos)
            {
                if (!context.SchemaRepository.Schemas.ContainsKey(baseSearchDto.ParameterType.ToString()))
                {
                    //Defino los esquemas asociados al objeto
                    OpenApiSchema generatedSchema = context.SchemaGenerator.GenerateSchema(baseSearchDto.ParameterType, context.SchemaRepository);

                    OpenApiParameter newParameter = new OpenApiParameter
                    {
                        Name = baseSearchDto.Name,
                        In = ParameterLocation.Query,
                        Schema = generatedSchema,
                        Required = true
                    };
                    List<OpenApiParameter> openApiParameters = new List<OpenApiParameter>();
                    openApiParameters.AddRange(GetNoQueryParameters(operation));
                    openApiParameters.Add(newParameter);
                    operation.Parameters = openApiParameters;
                }
            }
        }

    }

    private static IList<OpenApiParameter> GetNoQueryParameters(OpenApiOperation operation)
    {
        IList<OpenApiParameter> result = new List<OpenApiParameter>();
        foreach (OpenApiParameter parameter in operation.Parameters)
        {
            if (parameter.In != ParameterLocation.Query)
            {
                result.Add(parameter);
            }
        }
        return result;
    }
}
