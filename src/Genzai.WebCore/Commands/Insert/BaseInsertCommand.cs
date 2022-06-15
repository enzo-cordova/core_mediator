using Genzai.WebCore.Requests;
using Genzai.WebCore.Responses;
using MediatR;

namespace Genzai.WebCore.Commands.Insert;

/// <summary>
/// Base insert command
/// </summary>
/// <typeparam name="TEntityInsertRequest">Insert request</typeparam>
/// <typeparam name="TEntityResponse">Entity response</typeparam>
public abstract class BaseInsertCommand<TEntityInsertRequest, TEntityResponse> : IRequest<TEntityResponse>
    where TEntityInsertRequest : IEntityInsertRequest
    where TEntityResponse : IEntityResponse
{
    /// <summary>
    /// EntityInsertRequest
    /// </summary>
    public TEntityInsertRequest Request { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="request">Request</param>
    protected BaseInsertCommand(TEntityInsertRequest request)
    {
        Request = request;
    }
}
