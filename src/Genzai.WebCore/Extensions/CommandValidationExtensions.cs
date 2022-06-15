using FluentValidation;
using Genzai.WebCore.Errors;
using Genzai.WebCore.Exceptions;
using Genzai.WebCore.Validations;

namespace Genzai.WebCore.Extensions;

/// <summary>
/// Command validation extensions
/// </summary>
public static class CommandValidationExtensions
{
    /// <summary>
    /// Validate mediatr command.
    /// </summary>
    /// <typeparam name="TCommand">Mediatr Command</typeparam>
    /// <typeparam name="TEntity">TEntity</typeparam>
    /// <param name="validator">Command validator.</param>
    /// <param name="request">Mediatr Command</param>
    public static async Task ValidateEntityCommand<TEntity, TCommand>(this IValidator<TCommand> validator, TCommand request)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (validationResult != null && !validationResult.IsValid)
        {

            //Se parsean los errores y se lanza una excepcion
            IList<ApplicationError> errors = ValidationErrorUtils.GetValidationErrors<TEntity>(validationResult);
            throw new CoreValidationException(errors);
        }
    }
}
