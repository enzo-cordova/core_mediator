using Genzai.WebCore.Commands.Insert;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;

namespace Genzai.WebCore.Test.Mock.Application.Commands.Insert
{
    /// <summary>
    /// Command for insert sample handler
    /// </summary>
    public class InsertSampleCommand : BaseInsertCommand<SampleInsertRequest, SampleResponse>
    {
        public InsertSampleCommand(SampleInsertRequest request) : base(request)
        {
        }
    }
}