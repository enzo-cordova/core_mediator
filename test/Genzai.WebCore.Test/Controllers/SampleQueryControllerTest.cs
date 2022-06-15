using Genzai.WebCore.Test.Mock.Application.Controllers;
using Genzai.WebCore.Test.Mock.Application.Queries;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;
using Xunit;

namespace Genzai.WebCore.Test.Controllers
{
    public class SampleQueryControllerTest : BaseQueryControllerTest<SampleQueryController>
    {
        public SampleQueryControllerTest()
        {
            controller = new SampleQueryController(mediator.Object);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetSampleByIdTest(bool commandHandlerError)
        {
            this.GetEntityByIdTest<GetSampleByIdRequest, SampleResponse>(commandHandlerError);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void GetSampleSearchTest(bool commandHandlerError)
        {
            this.GetEntitySearchTest<GetSampleSearchRequest, SampleSearchRequest, SampleSearchResponse>(commandHandlerError);
        }
    }
}
