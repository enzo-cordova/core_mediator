using FluentValidation;
using Genzai.WebCore.Locales;
using System.Globalization;

namespace Genzai.WebCore.Commands.Delete;

/// <summary>
/// Base delete command validator
/// </summary>
/// <typeparam name="TDeleteCommand">Delete command</typeparam>
public abstract class BaseDeleteCommandValidation<TDeleteCommand> : AbstractValidator<TDeleteCommand>
    where TDeleteCommand : BaseDeleteCommand
{
    /// <summary>
    /// Constructor
    /// </summary>
    protected BaseDeleteCommandValidation()
    {
        //El id deber ser positivo
        RuleFor(property => property.Id)
            .NotNull()
            .GreaterThan(0)
            .WithMessage(string.Format(
                CultureInfo.InvariantCulture, WebCoreLocalStrings.ParameterIsNull, nameof(BaseDeleteCommand.Id)));
    }
}
