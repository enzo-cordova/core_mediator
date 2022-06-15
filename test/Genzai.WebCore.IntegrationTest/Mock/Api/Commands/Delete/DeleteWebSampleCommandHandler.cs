using Genzai.WebCore.Commands.Delete;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Test.Mock.Domain.Repositories;

namespace Genzai.WebCore.Integration.Test.Mock.Api.Commands.Delete
{
    /// <summary>
    /// DeleteSampleCommandHandler
    /// </summary>
    public class DeleteWebSampleCommandHandler : BaseDeleteCommandHandler<Sample, ISampleRepository, DeleteWebSampleCommand>
    {
        public DeleteWebSampleCommandHandler(ISampleRepository repository, IValidator<DeleteWebSampleCommand> validator) :
            base(repository, validator)
        {
        }
    }
}