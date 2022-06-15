namespace Genzai.Core.Extensions;

/// <summary>
/// Exception context extensions.
/// </summary>
public static class ExceptionContextExtensions
{
    /// <summary>
    /// ArgumentExceptionHandler.
    /// </summary>
    /// <param name="context">ExceptionContext</param>
    public static void ArgumentExceptionHandler(this ExceptionContext context)
    {
        var details = new ProblemDetails
        {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status400BadRequest,
            Detail = context.Exception.Message,
            Title = LocalStrings.ArgumentExceptionMessage
        };

        context.Result = new BadRequestObjectResult(details);
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }

    /// <summary>
    /// BadRequestHandler.
    /// </summary>
    /// <param name="context">ExceptionContext</param>
    /// <param name="environment">Environment</param>
    /// <param name="nonDevelopMessage">Non develop message.</param>
    public static void BadRequestHandler(
        this ExceptionContext context, IWebHostEnvironment environment, string nonDevelopMessage)
    {
        var details = new ProblemDetails
        {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status400BadRequest,
            Detail = environment.IsDevelopment() ? context.Exception.Message : nonDevelopMessage,
            Title = LocalStrings.ErrorMessageTitle
        };

        context.Result = new BadRequestObjectResult(details);
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }

    /// <summary>
    /// ServerErrorHandler.
    /// </summary>
    /// <param name="context">ExceptionContext</param>
    /// <param name="environment">Environment.</param>
    public static void ServerErrorHandler(
        this ExceptionContext context, IWebHostEnvironment environment)
    {
        var details = new ProblemDetails
        {
            Instance = context.HttpContext.Request.Path,
            Status = StatusCodes.Status500InternalServerError,
            Detail = environment.IsDevelopment() ? context.Exception.Message : LocalStrings.ServerErrorMessage,
            Title = LocalStrings.ErrorMessageTitle
        };

        context.Result = new BadRequestObjectResult(details);
        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }
}