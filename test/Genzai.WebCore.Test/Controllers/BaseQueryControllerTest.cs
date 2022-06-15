using Genzai.EfCore.Search;
using Genzai.WebCore.Controllers;
using Genzai.WebCore.Queries;
using Genzai.WebCore.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Genzai.WebCore.Test.Controllers
{
    public class BaseQueryControllerTest<TController> : BaseControllerTest<TController>
        where TController : BaseQueryController
    {


        public void GetEntityByIdTest<TGetEntityByIdQuery, TEntityResponse>(bool expectedError)
            where TGetEntityByIdQuery : GetEntityByIdQuery<TEntityResponse>
            where TEntityResponse : IEntityResponse
        {

            long id = 0;
            TEntityResponse response = (TEntityResponse)Activator.CreateInstance(typeof(TEntityResponse));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            if (!expectedError)
            {
                mediator.Setup(x => x.Send(It.IsAny<TGetEntityByIdQuery>(), default))
                    .Returns(Task.FromResult(response));

                Task<IActionResult> actionResultTask = this.ExecuteWithReflection(controller, "GetEntityById", new object[] { id });
                OkObjectResult actionResult = actionResultTask.Result as OkObjectResult;
                // Assert
                actionResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

            }
            else
            {
                mediator.Setup(x => x.Send(It.IsAny<TGetEntityByIdQuery>(), default))
                    .Throws(new Exception());
                Task<IActionResult> actionResultTask = this.ExecuteWithReflection(controller, "GetEntityById", new object[] { id });
                AggregateException exception = actionResultTask.Exception;
                exception.Should().NotBeNull();

            }
        }



        public void GetEntitySearchTest<TGetEntitySearchQuery, TEntitySearchRequest, TEntitySearchResponse>(bool expectedError)
            where TEntitySearchRequest : EntitySearch
            where TEntitySearchResponse : IEntitySearchResponse
            where TGetEntitySearchQuery : GetEntitySearchQuery<TEntitySearchRequest, TEntitySearchResponse>
        {
            TEntitySearchRequest searchRequest = (TEntitySearchRequest)Activator.CreateInstance(typeof(TEntitySearchRequest));

            List<TEntitySearchResponse> items = new List<TEntitySearchResponse>();
            PagedResponse<TEntitySearchResponse> response = (PagedResponse<TEntitySearchResponse>)Activator.CreateInstance(typeof(PagedResponse<TEntitySearchResponse>), items);

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext
            };

            if (!expectedError)
            {
                mediator.Setup(x => x.Send(It.IsAny<TGetEntitySearchQuery>(), default))
                    .Returns(Task.FromResult(response));

                Task<IActionResult> actionResultTask = this.ExecuteWithReflection(controller, "GetEntitySearchList", new object[] { searchRequest });
                OkObjectResult actionResult = actionResultTask.Result as OkObjectResult;
                // Assert
                actionResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

            }
            else
            {
                mediator.Setup(x => x.Send(It.IsAny<TGetEntitySearchQuery>(), default))
                    .Throws(new Exception());
                Task<IActionResult> actionResultTask = this.ExecuteWithReflection(controller, "GetEntitySearchList", new object[] { searchRequest });
                AggregateException exception = actionResultTask.Exception;
                exception.Should().NotBeNull();

            }
        }

    }
}
