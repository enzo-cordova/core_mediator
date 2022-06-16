using Genzai.WebCore.Commands.Insert;
using Genzai.WebCore.Errors;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Test.Mock.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Genzai.WebCore.Integration.Test.Mock.Api.Commands.Insert
{
    /// <summary>
    /// InsertSampleCommandHandler
    /// </summary>
    public class InsertWebSampleCommandHandler : BaseInsertCommandHandler<Sample, ISampleRepository,
            InsertWebSampleCommand, SampleInsertRequest, SampleResponse>
    {


        public InsertWebSampleCommandHandler(ISampleRepository repository, ILogger<InsertWebSampleCommand> logger,
            IValidator<InsertWebSampleCommand> validator, IMapper mapper) : base(repository, logger, validator, mapper)
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
    }
}