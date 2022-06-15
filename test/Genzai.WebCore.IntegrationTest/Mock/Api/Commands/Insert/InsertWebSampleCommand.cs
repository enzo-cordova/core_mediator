using Genzai.WebCore.Commands.Insert;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;

namespace Genzai.WebCore.Integration.Test.Mock.Api.Commands.Insert
{
    /// <summary>
    /// Command for insert sample handler
    /// </summary>
    public class InsertWebSampleCommand : BaseInsertCommand<SampleInsertRequest, SampleResponse>
    {
        public InsertWebSampleCommand(SampleInsertRequest request) : base(request)
        {
        }
    }
}