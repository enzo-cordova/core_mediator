using Genzai.WebCore.Commands.Delete;
using Genzai.WebCore.Commands.Insert;
using Genzai.WebCore.Commands.Updates;
using Genzai.WebCore.Requests;
using Genzai.WebCore.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Genzai.WebCore.Controllers;

/// <summary>
/// Base command controller
/// </summary>
public abstract class BaseCommandController : BaseController
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator">Mediator</param>
    /// <param name="tag">Tag</param>
    protected BaseCommandController(IMediator mediator, string tag) : base(mediator, tag)
    {
    }


    /// <summary>
    /// It inserts entity
    /// </summary>
    /// <typeparam name="TEntityInsertRequest">Entity insert request</typeparam>
    /// <typeparam name="TEntityResponse">Entity Response</typeparam>
    /// <typeparam name="TEntityInsertCommand">Insert command</typeparam>
    /// <param name="insertCommand">Insert command</param>
    /// <returns>Entity response</returns>
    public async Task<IActionResult> BaseInsertEntity<TEntityInsertRequest, TEntityResponse, TEntityInsertCommand>(TEntityInsertCommand insertCommand)
        where TEntityInsertCommand : BaseInsertCommand<TEntityInsertRequest, TEntityResponse>
        where TEntityInsertRequest : IEntityInsertRequest
        where TEntityResponse : IEntityResponse
    {
        return await CommandCreatedAsync(insertCommand);
    }

    /// <summary>
    /// It updates entity
    /// </summary>
    /// <typeparam name="TEntityUpdateRequest">Update request</typeparam>
    /// <typeparam name="TEntityUpdateCommand">Update command</typeparam>
    /// <param name="updateCommand">Update comamnd</param>
    /// <returns>Update result</returns>
    public async Task<IActionResult> BaseUpdateEntity<TEntityUpdateRequest, TEntityUpdateCommand>(TEntityUpdateCommand updateCommand)
        where TEntityUpdateCommand : BaseUpdateCommand<TEntityUpdateRequest>
        where TEntityUpdateRequest : IEntityUpdateRequest
    {
        return await CommandNoContentAsync(updateCommand);
    }

    /// <summary>
    /// It deletes entity
    /// </summary>
    /// <typeparam name="TEntityDeleteCommand">Delete command</typeparam>
    /// <param name="deleteCommand">Delete command</param>
    /// <returns>Delete result</returns>
    public async Task<IActionResult> BaseDeleteEntity<TEntityDeleteCommand>(TEntityDeleteCommand deleteCommand)
         where TEntityDeleteCommand : BaseDeleteCommand
    {
        return await CommandNoContentAsync(deleteCommand);
    }

}
