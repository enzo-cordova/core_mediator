using FluentValidation;
using Genzai.WebCore.Locales;
using Genzai.WebCore.Responses;
using System.Globalization;

namespace Genzai.WebCore.Queries;

/// <summary>
/// Validator for the command
/// </summary>
/// <typeparam name="TEntityResponse">Entity response</typeparam>
public abstract class GetEntityByIdQueryValidator<TEntityResponse> : AbstractValidator<GetEntityByIdQuery<TEntityResponse>>
    where TEntityResponse : IEntityResponse
{
    /// <summary>
    /// validator
    /// </summary>
    protected GetEntityByIdQueryValidator()
    {
        RuleFor(command => command.Id).GreaterThan(0).WithMessage(
            string.Format(CultureInfo.InvariantCulture, WebCoreLocalStrings.ParamEqualOrMoreThanZero, nameof(GetEntityByIdQuery<TEntityResponse>.Id)));
    }
}
