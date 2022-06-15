using Genzai.WebCore.Commands.Updates;
using Genzai.WebCore.Test.Mock.Application.Request;

namespace Genzai.WebCore.Integration.Test.Mock.Api.Commands.Updates
{
    /// <summary>
    /// UpdateSampleCommand
    /// </summary>
    public class UpdateWebSampleCommand : BaseUpdateCommand<SampleUpdateRequest>
    {
        public UpdateWebSampleCommand(long id, SampleUpdateRequest request) : base(id, request)
        {
        }
    }
}