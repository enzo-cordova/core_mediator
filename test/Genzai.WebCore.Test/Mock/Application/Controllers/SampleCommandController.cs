using Genzai.WebCore.Constants;
using Genzai.WebCore.Controllers;
using Genzai.WebCore.Test.Mock.Application.Commands.Delete;
using Genzai.WebCore.Test.Mock.Application.Commands.Insert;
using Genzai.WebCore.Test.Mock.Application.Commands.Updates;
using Genzai.WebCore.Test.Mock.Application.Constants;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Genzai.WebCore.Test.Mock.Application.Controllers
{
    /// <summary>
    /// Controller for Samples
    /// </summary>
    [ApiController]
    [Route($"{SampleConstants.PathBase}samples")]
    [ApiVersion("1.0")]
    [Produces(CommonsConstants.AplicationJson)]
    [Tags("Sample")]
    public class SampleCommandController : BaseCommandController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public SampleCommandController(IMediator mediator) : base(mediator, SampleConstants.ControllerSamples)
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
            return await BaseInsertEntity<SampleInsertRequest, SampleResponse, InsertSampleCommand>(new InsertSampleCommand(request));
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
            return await BaseUpdateEntity<SampleUpdateRequest, UpdateSampleCommand>(new UpdateSampleCommand(id, request));
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
            return await BaseDeleteEntity(new DeleteSampleCommand(id));
        }
    }
}