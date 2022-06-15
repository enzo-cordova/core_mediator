using Genzai.WebCore.Requests;
using MediatR;

namespace Genzai.WebCore.Commands.Updates;

/// <summary>
/// Base update command
/// </summary>
/// <typeparam name="TEntityUpdateRequest">Update request</typeparam>
public abstract class BaseUpdateCommand<TEntityUpdateRequest> : IRequest<bool>
    where TEntityUpdateRequest : IEntityUpdateRequest
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// Request
    /// </summary>
    public TEntityUpdateRequest Request { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="request">Request</param>
    protected BaseUpdateCommand(long id, TEntityUpdateRequest request)
    {
        Id = id;
        Request = request;
    }
}
