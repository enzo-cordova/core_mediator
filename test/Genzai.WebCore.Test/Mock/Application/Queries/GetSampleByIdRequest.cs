using Genzai.WebCore.Queries;
using Genzai.WebCore.Test.Mock.Application.Response;

namespace Genzai.WebCore.Test.Mock.Application.Queries
{
    public class GetSampleByIdRequest : GetEntityByIdQuery<SampleResponse>
    {
        public GetSampleByIdRequest(long id) : base(id)
        {
        }
    }
}
