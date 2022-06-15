using Genzai.WebCore.Commands.Delete;
using Genzai.WebCore.Interfaces;
using Genzai.WebCore.Locales;
using Genzai.WebCore.Test.Mock.Application.Response;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Test.Mock.Domain.Repositories;

namespace Genzai.WebCore.Integration.Test.Mock.Api.Commands.Delete
{
    /// <summary>
    /// DeleteSampleCommandValidator
    /// </summary>
    public class DeleteWebSampleCommandValidator : BaseDeleteCommandValidation<DeleteWebSampleCommand>
    {

        /// <summary>
        /// Constructor
        /// </summary>
        public DeleteWebSampleCommandValidator(ICacheService cacheService, ISampleRepository repository)
        {
            this.RuleFor(sample => sample.Id)
                .Must(id =>
                 {
                     SampleResponse response = cacheService.SearchAndFillIfNotExits<Sample, long, SampleResponse>("ID_SAMPLE", id =>
                          repository.GetByIdAsync(id), id).Result;
                     return response != null;
                 })
                 .When(sample => sample.Id != default(long))
                 .WithMessage(string.Format(WebCoreLocalStrings.InvalidValueMessage, nameof(DeleteWebSampleCommand.Id)))
                 .WithErrorCode("id.incorrect");


            this.RuleFor(sample => sample.Id)
                .Must(id =>
                 {
                     SampleResponse response = cacheService.SearchAndFillIfNotExits<Sample, long, SampleResponse>("ID_SAMPLE", id =>
                          repository.GetById(id), id);
                     return response != null;
                 })
                 .When(sample => sample.Id != default(long))
                 .WithMessage(string.Format(WebCoreLocalStrings.InvalidValueMessage, nameof(DeleteWebSampleCommand.Id)))
                 .WithErrorCode("id.incorrect");





        }

    }
}