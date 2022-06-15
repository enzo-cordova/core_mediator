using FluentValidation;
using Genzai.WebCore.Commands.Insert;
using Genzai.WebCore.Errors;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Test.Mock.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Genzai.WebCore.Test.Mock.Application.Commands.Insert
{
    /// <summary>
    /// InsertSampleCommandHandler
    /// </summary>
    public class InsertSampleCommandHandler : BaseInsertCommandHandler<Sample, ISampleRepository,
            InsertSampleCommand, SampleInsertRequest, SampleResponse>
    {


        public InsertSampleCommandHandler(ISampleRepository repository, ILogger<InsertSampleCommand> logger,
            IValidator<InsertSampleCommand> validator, IMapper mapper) : base(repository, logger, validator, mapper)
        {

        }

        protected override IList<ApplicationError> PreSaveValidation(Sample entity)
        {
            IList<ApplicationError> errors = new List<ApplicationError>();
            if (entity.SubSampleId <= 0)
            {
                errors.Add(new ApplicationError("submoduleerror",
              typeof(Sample).Name.ToLower(), "sample.submoduleid.incorrect", null));
            }
            return errors;
        }

        /// <summary>
        /// Entity from request
        /// </summary>
        /// <param name="request">Request</param>
        /// <returns>Entity</returns>
        protected override Sample GetEntityFromRequest(InsertSampleCommand request)
        {
            return _mapper.Map<Sample>(request.Request);
        }
    }
}