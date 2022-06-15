using Genzai.WebCore.Commands.Delete;
using Genzai.WebCore.Commands.Insert;
using Genzai.WebCore.Commands.Updates;
using Genzai.WebCore.Controllers;
using Genzai.WebCore.Requests;
using Genzai.WebCore.Responses;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;

namespace Genzai.WebCore.Test.Controllers
{
    public class BaseCommandControllerTest<TController> : BaseControllerTest<TController>
        where TController : BaseCommandController
    {



        public void InsertEntityTest<TEntityInsertRequest, TEntityResponse, TEntityInsertCommand>(
             bool expectedError)
            where TEntityInsertCommand : BaseInsertCommand<TEntityInsertRequest, TEntityResponse>
            where TEntityInsertRequest : IEntityInsertRequest
            where TEntityResponse : IEntityResponse
        {

            TEntityInsertRequest request = (TEntityInsertRequest)Activator.CreateInstance(typeof(TEntityInsertRequest));
            TEntityResponse response = (TEntityResponse)Activator.CreateInstance(typeof(TEntityResponse));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext

            };

            if (!expectedError)
            {
                mediator.Setup(x => x.Send(It.IsAny<TEntityInsertCommand>(), default))
                    .Returns(Task.FromResult(response));

                Task<IActionResult> actionResultTask = this.ExecuteWithReflection(controller, "InsertEntity", new object[] { request });
                CreatedResult actionResult = actionResultTask.Result as CreatedResult;
                // Assert
                actionResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);

            }
            else
            {
                mediator.Setup(x => x.Send(It.IsAny<TEntityInsertCommand>(), default))
                    .Throws(new Exception());


                Task<IActionResult> actionResultTask = this.ExecuteWithReflection(controller, "InsertEntity", new object[] { request });
                AggregateException exception = actionResultTask.Exception;
                exception.Should().NotBeNull();

            }
        }


        public void UpdateEntityTest<TEntityUpdateRequest, TEntityUpdateCommand>(
            bool expectedError)
           where TEntityUpdateCommand : BaseUpdateCommand<TEntityUpdateRequest>
           where TEntityUpdateRequest : IEntityUpdateRequest
        {

            TEntityUpdateRequest request = (TEntityUpdateRequest)Activator.CreateInstance(typeof(TEntityUpdateRequest));
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext

            };

            if (!expectedError)
            {
                mediator.Setup(x => x.Send(It.IsAny<TEntityUpdateCommand>(), default))
                    .Returns(Task.FromResult(true));

                Task<IActionResult> actionResultTask = this.ExecuteWithReflection(controller, "UpdateEntity", new object[] { 0, request });
                NoContentResult actionResult = actionResultTask.Result as NoContentResult;
                // Assert
                actionResult?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

            }
            else
            {
                mediator.Setup(x => x.Send(It.IsAny<TEntityUpdateCommand>(), default))
                    .Throws(new Exception());


                Task<IActionResult> actionResultTask = this.ExecuteWithReflection(controller, "UpdateEntity", new object[] { 0, request });
                AggregateException exception = actionResultTask.Exception;
                exception.Should().NotBeNull();

            }
        }



        public void DeleteEntityTest<TEntityDeleteCommand>(
            bool expectedError)
           where TEntityDeleteCommand : BaseDeleteCommand
        {

            long id = 0;
            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = httpContext

            };

            if (!expectedError)
            {
                mediator.Setup(x => x.Send(It.IsAny<TEntityDeleteCommand>(), default))
                    .Returns(Task.FromResult(true));

                Task<IActionResult> actionResultTask = this.ExecuteWithReflection(controller, "DeleteEntity", new object[] { id });
                NoContentResult actionResult = actionResultTask.Result as NoContentResult;
                // Assert
                actionResult?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

            }
            else
            {
                mediator.Setup(x => x.Send(It.IsAny<TEntityDeleteCommand>(), default))
                    .Throws(new Exception());


                Task<IActionResult> actionResultTask = this.ExecuteWithReflection(controller, "DeleteEntity", new object[] { id });
                AggregateException exception = actionResultTask.Exception;
                exception.Should().NotBeNull();

            }
        }

    }
}
