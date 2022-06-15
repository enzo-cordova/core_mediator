using Genzai.WebCore.Attributes;
using Genzai.WebCore.Errors;
using Genzai.WebCore.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.ObjectModel;

namespace Genzai.WebCore.Swagger;

public class SwaggerResponsesOperationFilter : IOperationFilter
{
    private const string errorBean = "ApplicationError";
    public static readonly IList<string> Methods404 = new ReadOnlyCollection<string>(new List<string> { "DeleteEntity", "UpdateEntity", "PartialUpdateEnity", "GetEntityById" });
    public static readonly IList<string> Methods400 = new ReadOnlyCollection<string>(new List<string> { "InsertEntity", "UpdateEntity", "PartialUpdateEnity" });
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Responses.Add("500", new OpenApiResponse { Description = "Internal server Error" });

        if (AttributeUtils.HaveAttribute<AuthorizeAttribute>(context.MethodInfo))
        {
            operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
        }

        if ((NeedError404(context) || NeedError400(context)) && !context.SchemaRepository.Schemas.ContainsKey(errorBean))
        {

            CreateErrorResponse(context);

        }
        if (NeedError404(context))
        {
            AddErrorResponse(operation, context, "404", "Not found");
        }
        if (NeedError400(context))
        {
            AddErrorResponse(operation, context, "400", "Bad Request");
        }
    }

    private static void AddErrorResponse(OpenApiOperation operation, OperationFilterContext context, string code, string description)
    {
        OpenApiResponse data = new OpenApiResponse
        {
            Description = description,
            Content = new Dictionary<string, OpenApiMediaType>
            {
                ["application/json"] = new OpenApiMediaType
                {
                    Schema = context.SchemaRepository.Schemas[errorBean]
                }
            }
        };
        operation.Responses.Add(code, data);
    }

    private static void CreateErrorResponse(OperationFilterContext context)
    {
        context.SchemaGenerator.GenerateSchema(typeof(ApplicationError), context.SchemaRepository);
    }

    private static bool NeedError400(OperationFilterContext context)
    {
        bool methodError400 = Methods400.Contains(context.MethodInfo.Name);
        bool error400Attribute = AttributeUtils.HaveAttribute<Error400Attribute>(context.MethodInfo);
        return methodError400 || error400Attribute;
    }

    private static bool NeedError404(OperationFilterContext context)
    {
        return Methods404.Contains(context.MethodInfo.Name);
    }
}
