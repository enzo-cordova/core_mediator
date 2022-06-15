using Genzai.WebCore.Commands.Updates;
using Genzai.WebCore.Test.Mock.Application.Request;

namespace Genzai.WebCore.Test.Mock.Application.Commands.Updates
{
    /// <summary>
    /// UpdateSampleCommand
    /// </summary>
    public class UpdateSampleCommand : BaseUpdateCommand<SampleUpdateRequest>
    {
        public UpdateSampleCommand(long id, SampleUpdateRequest request) : base(id, request)
        {
        }
    }
}