namespace Genzai.Core.Controller;

/// <summary>
/// BaseController
/// </summary>
[ApiController]
[Produces("application/json")]
public class BaseController : ControllerBase
{
    private readonly IMediator _mediator;

    private readonly string _tag;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="tag"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public BaseController(IMediator mediator, string tag)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _tag = tag;
    }

    /// <summary>
    /// QueryAsync
    /// </summary>
    /// <param name="query"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
    protected async Task<TResult> QueryAsync<TResult>(IRequest<TResult> query)
    {
        AddTag();
        return await _mediator.Send(query);
    }

    /// <summary>
    /// Single
    /// </summary>
    /// <param name="data"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected IActionResult Single<T>(T data)
    {
        return Ok(data);
    }

    /// <summary>
    /// TResult
    /// </summary>
    /// <param name="command"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
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
    /// <param name="command"></param>
    /// <typeparam name="TResult"></typeparam>
    /// <returns></returns>
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