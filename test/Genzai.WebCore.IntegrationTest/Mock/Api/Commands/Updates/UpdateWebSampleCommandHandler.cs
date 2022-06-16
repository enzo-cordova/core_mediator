using Genzai.WebCore.Commands.Updates;
using Genzai.WebCore.Errors;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Test.Mock.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Genzai.WebCore.Integration.Test.Mock.Api.Commands.Updates
{
    /// <summary>
    /// UpdateSampleCommandHandler
    /// </summary>
    public class UpdateWebSampleCommandHandler : BaseUpdateCommandHandler<Sample, ISampleRepository,
        UpdateWebSampleCommand, SampleUpdateRequest>

    {
        public UpdateWebSampleCommandHandler(ISampleRepository repository, IValidator<UpdateWebSampleCommand> validator, IMapper mapper) : base(repository, validator, mapper)
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

        //protected override async Task<bool> SaveAuditableAsync(CancellationToken cancellationToken)
        //{
        //    return await this._repository.SaveAuditableAsync(cancellationToken);
        //}
    }
}