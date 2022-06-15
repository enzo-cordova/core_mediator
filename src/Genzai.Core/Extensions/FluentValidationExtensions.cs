namespace Genzai.Core.Extensions;

/// <summary>
/// Extensions for Fluent Validations.
/// </summary>
public static class FluentValidationExtensions
{
    /// <summary>
    /// Validate mediatr command.
    /// </summary>
    /// <typeparam name="TCommand">Mediatr Command</typeparam>
    /// <param name="validator">Command validator.</param>
    /// <param name="request">Mediatr Command</param>
    public static async Task ValidateCommand<TCommand>(this IValidator<TCommand> validator, TCommand request)
    {
        var validatorResult = await validator.ValidateAsync(request);

        if (!validatorResult.IsValid)
        {
            var messages = validatorResult.Errors.Select(message => message.ErrorMessage).ToArray();

            throw new ArgumentException(string.Join(", ", messages));
        }
    }
}