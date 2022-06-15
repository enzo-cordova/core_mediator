using FluentValidation;
using Genzai.WebCore.Locales;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Validations;

namespace Genzai.WebCore.Test.Mock.Application.Commands.Insert
{
    /// <summary>
    /// InsertSampleCommandValidator
    /// </summary>
    public class InsertSampleCommandValidator : BaseAbstractValidator<InsertSampleCommand>
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public InsertSampleCommandValidator()
        {

            //Name
            this.AppendRuleRequired(sample => sample.Request.Name, nameof(Sample.Name));
            this.AppendRuleRequiredWithRestriction(sample => sample.Request.Name, sample => sample.Request.Name != null, nameof(Sample.Name));

            this.AppendRuleLength(sample => sample.Request.Name, ValidatorConstants.String32MaxLength, nameof(Sample.Name));
            this.AppendRuleLengthWithRestriction(sample => sample.Request.Name, sample => sample.Request.Name != null,
              ValidatorConstants.String32MaxLength, nameof(Sample.Name));


            this.RuleFor(sample => sample.Request.Name).NotNull().
            WithMessage(string.Format(WebCoreLocalStrings.RequiredFieldMessage, nameof(Sample.Name))).
              WithErrorCode(string.Format("name.required;test={0}", "test"));


        }

    }
}