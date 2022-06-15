using Genzai.WebCore.Interfaces;
using Genzai.WebCore.Locales;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Validations;

namespace Genzai.WebCore.Integration.Test.Mock.Api.Commands.Insert
{
    /// <summary>
    /// InsertSampleCommandValidator
    /// </summary>
    public class InsertWebSampleCommandValidator : BaseAbstractValidator<InsertWebSampleCommand>
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public InsertWebSampleCommandValidator(ICacheService cacheService)
        {

            //Name
            this.AppendRuleRequired(sample => sample.Request.Name, nameof(Sample.Name));
            this.AppendRuleLength(sample => sample.Request.Name, ValidatorConstants.String32MaxLength, nameof(Sample.Name));

            this.RuleFor(sample => sample.Request.Name).NotNull().
            WithMessage(string.Format(WebCoreLocalStrings.RequiredFieldMessage, nameof(Sample.Name))).
              WithErrorCode(string.Format("name.required;test={0}", "test"));



        }


    }
}