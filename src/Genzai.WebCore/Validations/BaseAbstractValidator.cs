
using FluentValidation;
using Genzai.WebCore.Locales;
using System.Linq.Expressions;

namespace Genzai.WebCore.Validations;

/// <summary>
/// Base abstract validator
/// </summary>
/// <typeparam name="T">Validator type</typeparam>
public class BaseAbstractValidator<T> : AbstractValidator<T>
{
    protected void AppendRuleLength(Expression<Func<T, string>> expression, int length, string paramName)
    {
        RuleFor(expression).MaximumLength(length).
           WithMessage(string.Format(WebCoreLocalStrings.MaxLengthMessage, paramName, length)).
           WithErrorCode(string.Format("{0}.maxlength;maxlength={1}", paramName, length));
    }

    protected void AppendRuleLengthWithRestriction(Expression<Func<T, string>> expression, Func<T, bool> when, int length, string paramName)
    {
        RuleFor(expression).MaximumLength(length).
           When(when).
           WithMessage(string.Format(WebCoreLocalStrings.MaxLengthMessage, paramName, length)).
           WithErrorCode(string.Format("{0}.maxlength;maxlength={1}", paramName, length));
    }

    protected void AppendRuleRequiredWithRestriction(Expression<Func<T, string>> expression, Func<T, bool> when, string paramName)
    {
        RuleFor(expression).NotEmpty()
            .When(when)
            .WithMessage(string.Format(WebCoreLocalStrings.RequiredFieldMessage, paramName)).
            WithErrorCode(string.Format("{0}.empty", paramName));
    }

    protected void AppendRuleRequired<ExpressionElement>(Expression<Func<T, ExpressionElement>> expression, string paramName)
    {
        RuleFor(expression).NotNull().
            WithMessage(string.Format(WebCoreLocalStrings.RequiredFieldMessage, paramName)).
            WithErrorCode(string.Format("{0}.empty", paramName));
    }
}
