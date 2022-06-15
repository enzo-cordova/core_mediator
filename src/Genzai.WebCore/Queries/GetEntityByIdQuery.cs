using Genzai.WebCore.Responses;
using MediatR;

namespace Genzai.WebCore.Queries;

/// <summary>
/// Get entity by id request
/// </summary>
/// <typeparam name="TEntityResponse">Entity response</typeparam>
public abstract class GetEntityByIdQuery<TEntityResponse> : IRequest<TEntityResponse>
    where TEntityResponse : IEntityResponse
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="id">Id</param>
    protected GetEntityByIdQuery(long id)
    {
        Id = id;
    }

    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }
}
