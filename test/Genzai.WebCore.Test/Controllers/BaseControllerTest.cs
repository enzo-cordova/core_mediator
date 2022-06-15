using Genzai.WebCore.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace Genzai.WebCore.Test.Controllers
{

    public class BaseControllerTest<TController>
        where TController : BaseController
    {

        protected readonly Mock<IMediator> mediator;
        protected readonly Mock<ILogger<TController>> logger;

        protected DefaultHttpContext httpContext;
        protected TController controller;

        /// <summary>
        /// Ctor
        /// </summary>
        public BaseControllerTest()
        {
            mediator = new Mock<IMediator>();
            logger = new Mock<ILogger<TController>>();

            httpContext = new DefaultHttpContext();
        }


        protected Task<IActionResult> ExecuteWithReflection(TController controller, string methodName, object[] parameterObjects)
        {
            MethodInfo methodInfo = controller!.GetType().GetMethod(methodName)!;
            return (Task<IActionResult>)methodInfo.Invoke(controller, parameterObjects)!;
        }
    }
}
