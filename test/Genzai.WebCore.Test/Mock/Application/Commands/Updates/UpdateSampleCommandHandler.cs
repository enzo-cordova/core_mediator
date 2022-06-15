using FluentValidation;
using Genzai.WebCore.Commands.Updates;
using Genzai.WebCore.Errors;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Test.Mock.Domain.Repositories;

namespace Genzai.WebCore.Test.Mock.Application.Commands.Updates
{
    /// <summary>
    /// UpdateSampleCommandHandler
    /// </summary>
    public class UpdateSampleCommandHandler : BaseUpdateCommandHandler<Sample, ISampleRepository,
        UpdateSampleCommand, SampleUpdateRequest>

    {
        public UpdateSampleCommandHandler(ISampleRepository repository, IValidator<UpdateSampleCommand> validator, IMapper mapper) : base(repository, validator, mapper)
        {

        }

        protected override IList<ApplicationError> PreUpdateValidation(Sample oldFromDatabase, SampleUpdateRequest updateRequest)
        {
            return new List<ApplicationError>();
        }

        protected override IList<ApplicationError> PreUpdateValidation(Sample newEntity)
        {
            IList<ApplicationError> errors = new List<ApplicationError>();
            if (newEntity.SubSampleId <= 0)
            {
                errors.Add(new ApplicationError("submoduleerror",
              typeof(Sample).Name.ToLower(), "sample.submoduleid.incorrect", null));
            }
            return errors;
        }
    }
}