using Genzai.EfCore.Search;
using Genzai.WebCore.Responses;
using MediatR;

namespace Genzai.WebCore.Queries;

/// <summary>
/// Entity search query
/// </summary>
/// <typeparam name="TEntitySearchRequest">Search request</typeparam>
/// <typeparam name="TEntitySearchResponse">Search response</typeparam>
public abstract class GetEntitySearchQuery<TEntitySearchRequest, TEntitySearchResponse> : IRequest<PagedResponse<TEntitySearchResponse>>
    where TEntitySearchRequest : EntitySearch
    where TEntitySearchResponse : IEntitySearchResponse
{
    /// <summary>
    /// Request from controller
    /// </summary>
    public TEntitySearchRequest SearchRequest { get; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="searchRequest">Search request</param>
    protected GetEntitySearchQuery(TEntitySearchRequest searchRequest)
    {
        SearchRequest = searchRequest;
    }
}
