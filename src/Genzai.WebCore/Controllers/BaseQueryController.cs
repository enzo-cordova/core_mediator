using Genzai.EfCore.Search;
using Genzai.WebCore.Queries;
using Genzai.WebCore.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Genzai.WebCore.Controllers;

/// <summary>
/// Base query controller
/// </summary>
public abstract class BaseQueryController : BaseController
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="mediator">Mediator</param>
    /// <param name="tag">Tag</param>
    protected BaseQueryController(IMediator mediator, string tag) : base(mediator, tag)
    {
    }


    /// <summary>
    /// It returns entity by id
    /// </summary>
    /// <typeparam name="TGetEntityByIdRequest">Entity by id request</typeparam>
    /// <typeparam name="TEntityResponse">Entity response</typeparam>
    /// <param name="getEntityByIdRequest">Entity by id request</param>
    /// <returns>Enity</returns>
    public async Task<IActionResult> BaseGetEntityById<TGetEntityByIdRequest, TEntityResponse>(TGetEntityByIdRequest getEntityByIdRequest)
        where TGetEntityByIdRequest : GetEntityByIdQuery<TEntityResponse>
        where TEntityResponse : IEntityResponse
    {
        return Single(await QueryAsync(getEntityByIdRequest));
    }

    /// <summary>
    /// It search entitys that match with search
    /// </summary>
    /// <typeparam name="TGetEntitySearchRequest">Get entity search request</typeparam>
    /// <typeparam name="TEntitySearchRequest">Entity search request</typeparam>
    /// <typeparam name="TEntitySearchResponse">Entity search response</typeparam>
    /// <param name="getEntitySearchRequest">Request</param>
    /// <returns>Result of search</returns>
    public async Task<IActionResult> BaseGetEntitySearchList<TGetEntitySearchRequest, TEntitySearchRequest, TEntitySearchResponse>(
        TGetEntitySearchRequest getEntitySearchRequest)
        where TGetEntitySearchRequest : GetEntitySearchQuery<TEntitySearchRequest, TEntitySearchResponse>
        where TEntitySearchRequest : EntitySearch
        where TEntitySearchResponse : IEntitySearchResponse
    {
        return Single(await QueryAsync(getEntitySearchRequest));
    }
}
