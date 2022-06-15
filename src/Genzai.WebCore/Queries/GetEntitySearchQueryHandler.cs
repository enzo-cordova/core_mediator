using AutoMapper;
using FluentValidation;
using Genzai.Core.Domain.Model;
using Genzai.EfCore.Repository;
using Genzai.EfCore.Search;
using Genzai.WebCore.Extensions;
using Genzai.WebCore.Responses;
using MediatR;

namespace Genzai.WebCore.Queries;

/// <summary>
/// Enity search query handler
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
/// <typeparam name="TEntitySearch">Entity search</typeparam>
/// <typeparam name="TEntitySearchResult">Entity search result</typeparam>
/// <typeparam name="TRepository">Repository</typeparam>
/// <typeparam name="TEntitySearchRequest">Entity search request</typeparam>
/// <typeparam name="TEntitySearchQuery">Entity search query</typeparam>
/// <typeparam name="TEntitySearchResponse">Entity search response</typeparam>
public abstract class GetEntitySearchQueryHandler<TEntity, TEntitySearch, TEntitySearchResult, TRepository, TEntitySearchRequest, TEntitySearchQuery, TEntitySearchResponse> :
    IRequestHandler<TEntitySearchQuery, PagedResponse<TEntitySearchResponse>>
    where TRepository : IPartialSearchRepository<TEntity, long, TEntitySearch, TEntitySearchResult>
    where TEntity : class, IEntity<long>
    where TEntitySearchRequest : EntitySearch
    where TEntitySearchResponse : IEntitySearchResponse
    where TEntitySearchQuery : GetEntitySearchQuery<TEntitySearchRequest, TEntitySearchResponse>
    where TEntitySearch : EntitySearch
    where TEntitySearchResult : EntityIdLongSearchResult
{
    protected readonly TRepository _repository;
    protected readonly IMapper _mapper;
    protected readonly IValidator<TEntitySearchQuery> _validator;


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">Repository</param>
    /// <param name="validator">Validator</param>
    /// <param name="mapper">Mapper</param>
    protected GetEntitySearchQueryHandler(TRepository repository,
        IValidator<TEntitySearchQuery> validator, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Handle Operations
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Operation result</returns>
    public virtual async Task<PagedResponse<TEntitySearchResponse>> Handle(TEntitySearchQuery request, CancellationToken cancellationToken)
    {
        //Validate request
        await _validator.ValidateEntityCommand<TEntity, TEntitySearchQuery>(request);
        //Entity search
        TEntitySearch entitySearch = GetEntitySearchFromRequest(request);
        //Search
        Task<List<TEntitySearchResult>> list = _repository.Search(entitySearch);
        IEnumerable<TEntitySearchResponse> items = GetItems(list.Result);
        //Search size
        int size = _repository.SearchSize(entitySearch).Result;

        //Paged response
        return new PagedResponse<TEntitySearchResponse>(items)
        {
            PageNumber = request.SearchRequest.PageNumber,
            PageSize = request.SearchRequest.PageSize,
            SearchSize = size,
            Items = items
        };
    }

    /// <summary>
    /// It returns entity search from request
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    protected virtual TEntitySearch GetEntitySearchFromRequest(TEntitySearchQuery request)
    {
        return _mapper.Map<TEntitySearch>(request.SearchRequest);
    }

    /// <summary>
    /// It returns entity search response list from entity search result list
    /// </summary>
    /// <param name="list">Result list</param>
    /// <returns>Entity search response list</returns>
    protected virtual IEnumerable<TEntitySearchResponse> GetItems(IEnumerable<TEntitySearchResult> list)
    {
        List<TEntitySearchResponse> result = new List<TEntitySearchResponse>();
        if (list != null)
        {
            foreach (TEntitySearchResult item in list)
            {
                TEntitySearchResponse itemResponse = _mapper.Map<TEntitySearchResponse>(item);
                result.Add(itemResponse);
            }
        }
        return result;
    }
}
