using Genzai.EfCore.Constants;
using Genzai.WebCore.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Genzai.WebCore.Controllers;

/// <summary>
/// BaseController
/// </summary>
[ApiController]
[Produces(CommonsConstants.AplicationJson)]
public class BaseController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly string _tag;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="mediator">Mediator</param>
    /// <param name="tag">Tag</param>
    public BaseController(IMediator mediator, string tag)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _tag = tag;
    }

    /// <summary>
    /// QueryAsync
    /// </summary>
    /// <param name="query">Query</param>
    /// <typeparam name="TResult">Result</typeparam>
    /// <returns>Query result</returns>
    protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
    {
        AddTag();
        return await _mediator.Send(query);
    }

    /// <summary>
    /// Single
    /// </summary>
    /// <param name="data">Data</param>
    /// <typeparam name="T">Data type</typeparam>
    /// <returns>Result</returns>
    protected IActionResult Single<T>(T data)
    {
        return Ok(data);
    }

    /// <summary>
    /// TResult
    /// </summary>
    /// <param name="command">Command</param>
    /// <typeparam name="TResult">Result</typeparam>
    /// <returns>Command result</returns>
    protected async Task<IActionResult> CommandNoContentAsync<TResult>(IRequest<TResult> command)
    {
        var result = await _mediator.Send(command);
        if (result != null && Convert.ToBoolean(result))
            return new NoContentResult();
        return BadRequest(false);
    }

    /// <summary>
    /// TResult
    /// </summary>
    /// <param name="command">Command</param>
    /// <typeparam name="TResult">Result</typeparam>
    /// <returns>Command result</returns>
    protected async Task<IActionResult> CommandCreatedAsync<TResult>(IRequest<TResult> command)
    {
        var result = await _mediator.Send(command);
        return Created("", result);
    }

    private void AddTag()
    {
        HttpContext.Request.RouteValues.Add("Tag", _tag);
    }
}
