using Genzai.EfCore.Constants;
using Genzai.WebCore.Constants;
using Genzai.WebCore.Controllers;
using Genzai.WebCore.Integration.Test.Mock.Api.Commands.Delete;
using Genzai.WebCore.Integration.Test.Mock.Api.Commands.Insert;
using Genzai.WebCore.Integration.Test.Mock.Api.Commands.Updates;
using Genzai.WebCore.Test.Mock.Application.Constants;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;
using MediatR;
using System.Threading.Tasks;

namespace Genzai.WebCore.Integration.Test.Mock.Api.Controllers
{
    /// <summary>
    /// Controller for Samples
    /// </summary>
    [ApiController]
    [Route($"{SampleConstants.PathBase}samples")]
    [ApiVersion("1.0")]
    [Produces(CommonsConstants.AplicationJson)]
    [Tags("Sample")]
    public class WebSampleCommandController : BaseCommandController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public WebSampleCommandController(IMediator mediator) : base(mediator, SampleConstants.ControllerSamples)
        {
        }

        /// <summary>
        /// Insert Sample
        /// </summary>
        /// <returns></returns>
        [HttpPost(
            "",
           Name = "insertSample"
            )]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SampleResponse))]
        public async Task<IActionResult> InsertEntity([FromBody] SampleInsertRequest request)
        {

            return await BaseInsertEntity<SampleInsertRequest, SampleResponse, InsertWebSampleCommand>(new InsertWebSampleCommand(request));

        }

        /// <summary>
        /// Update Sample
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        [HttpPut(
            "{id}",
              Name = "updateSample"
            )]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateEntity(long id, [FromBody] SampleUpdateRequest request)
        {
            return await BaseUpdateEntity<SampleUpdateRequest, UpdateWebSampleCommand>(
                new UpdateWebSampleCommand(id, request));
        }

        /// <summary>
        /// It deletes an sample
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete(
            "{id}",
            Name = "deleteSample"
            )]
        public async Task<IActionResult> DeleteEntity(long id)
        {
            return await BaseDeleteEntity(new DeleteWebSampleCommand(id));
        }
    }
}