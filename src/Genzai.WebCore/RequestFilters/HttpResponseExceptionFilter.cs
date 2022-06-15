using Genzai.WebCore.Errors;
using Genzai.WebCore.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Genzai.WebCore.RequestFilters;

/// <summary>
/// Response exception filter
/// </summary>
public class HttpResponseExceptionFilter : IActionFilter, IOrderedFilter
{
    private const string IntenalErrorMessage = "Internal error";
    private const string ForbiddenMessage = "Forbidden resource";
    private const int BadRequest = 400;
    private const int Forbidden = 403;
    private const int NotFound = 404;
    private const int InternalError = 500;

    private readonly ILogger _logger;

    public int Order { get; } = int.MaxValue - 1000;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="loggerFactory">Logger factory</param>
    public HttpResponseExceptionFilter(ILoggerFactory loggerFactory)
    {
        this._logger = loggerFactory.CreateLogger(this.GetType().ToString());

    }

    /// <inheritdoc/>
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Do nothing
    }

    /// <inheritdoc/>
    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception != null)
        {
            Exception exception = context.Exception;
            if (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
            //Mensaje
            object result = null;
            if (exception is ApplicationErrorException gedgeException)
            {
                result = gedgeException.Errors;
            }
            else
            {
                result = new List<ApplicationError> { new ApplicationError(IntenalErrorMessage, null, null, null) };
            }

            //codigo
            int statusCode = InternalError;
            if (exception is CoreValidationException)
            {
                statusCode = BadRequest;
            }
            else if (exception is EntityNotFoundException)
            {
                statusCode = NotFound;
            }
            else if (exception is ApplicationErrorException)
            {
                statusCode = BadRequest;
            }
            else if (exception is UnauthorizedException)
            {
                statusCode = Forbidden;
                result = new List<ApplicationError> { new ApplicationError(ForbiddenMessage, null, null, null) };
            }
            else
            {
                this._logger.LogError(exception, IntenalErrorMessage);
            }

            context.Result = new ObjectResult(result)
            {
                StatusCode = statusCode,
            };
            context.ExceptionHandled = true;

        }
    }
}
