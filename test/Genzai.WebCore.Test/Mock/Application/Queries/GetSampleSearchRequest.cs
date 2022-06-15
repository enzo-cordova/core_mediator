using Genzai.WebCore.Queries;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;

namespace Genzai.WebCore.Test.Mock.Application.Queries
{
    public class GetSampleSearchRequest : GetEntitySearchQuery<SampleSearchRequest, SampleSearchResponse>
    {
        public GetSampleSearchRequest(SampleSearchRequest parameters) : base(parameters)
        {
        }
    }
}
