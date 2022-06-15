using FluentValidation;
using Genzai.WebCore.Commands.Delete;
using Genzai.WebCore.Test.Mock.Domain.Persistence.Model;
using Genzai.WebCore.Test.Mock.Domain.Repositories;

namespace Genzai.WebCore.Test.Mock.Application.Commands.Delete
{
    /// <summary>
    /// DeleteSampleCommandHandler
    /// </summary>
    public class DeleteSampleCommandHandler : BaseDeleteCommandHandler<Sample, ISampleRepository, DeleteSampleCommand>
    {
        public DeleteSampleCommandHandler(ISampleRepository repository, IValidator<DeleteSampleCommand> validator) :
            base(repository, validator)
        {
        }
    }
}