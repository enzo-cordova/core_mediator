using Genzai.EfCore.Constants;
using Genzai.WebCore.Constants;
using Genzai.WebCore.Controllers;
using Genzai.WebCore.Responses;
using Genzai.WebCore.Test.Mock.Application.Constants;
using Genzai.WebCore.Test.Mock.Application.Queries;
using Genzai.WebCore.Test.Mock.Application.Request;
using Genzai.WebCore.Test.Mock.Application.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    public class SampleQueryController : BaseQueryController
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mediator"></param>
        public SampleQueryController(IMediator mediator) : base(mediator, SampleConstants.ControllerSamples)
        {
        }

        /// <summary>
        /// It returns an sample
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}",
            Name = "getSample"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SampleResponse))]
        public async Task<IActionResult> GetEntityById(long id)
        {
            return await BaseGetEntityById<GetSampleByIdRequest, SampleResponse>(new GetSampleByIdRequest(id));
        }


        /// <summary>
        /// It search samples that match with search
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns>Sample paginated results</returns>
        [HttpGet("filter",
                Name = "searchSamples"
        )]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResponse<SampleSearchResponse>))]
        public async Task<IActionResult> GetEntitySearchList(
            [FromQuery, SwaggerParameter(Required = true)]
            SampleSearchRequest searchRequest)
        {
            return await BaseGetEntitySearchList<GetSampleSearchRequest, SampleSearchRequest, SampleSearchResponse>(new GetSampleSearchRequest(searchRequest));
        }
    }
}

