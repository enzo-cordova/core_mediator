using Genzai.WebCore.Test.Mock.Application.Commands.Delete;
using Genzai.WebCore.Test.Mock.Application.Commands.Insert;
using Genzai.WebCore.Test.Mock.Application.Commands.Updates;
using Genzai.WebCore.Test.Mock.Application.Controllers;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;
using Xunit;

namespace Genzai.WebCore.Test.Controllers
{
    public class SampleCommandControllerTest : BaseCommandControllerTest<SampleCommandController>
    {
        public SampleCommandControllerTest()
        {
            controller = new SampleCommandController(mediator.Object);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void InsertSampleTest(bool commandHandlerError)
        {
            this.InsertEntityTest<SampleInsertRequest, SampleResponse, InsertSampleCommand>(commandHandlerError);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void UpdateSampleTest(bool commandHandlerError)
        {
            this.UpdateEntityTest<SampleUpdateRequest, UpdateSampleCommand>(commandHandlerError);
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void DeleteSampleTest(bool commandHandlerError)
        {
            this.DeleteEntityTest<DeleteSampleCommand>(commandHandlerError);
        }
    }
}
