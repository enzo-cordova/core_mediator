using Genzai.EfCore.Context;
using Genzai.EfCore.Search;
using Genzai.EfCore.Utils;
using LinqKit;

namespace Genzai.EfCore.Repository;

/// <summary>
/// Repository for partial search
/// </summary>
/// <typeparam name="TContext">Context</typeparam>
/// <typeparam name="TEntity">Entity</typeparam>
/// <typeparam name="TKey">Key</typeparam>
/// <typeparam name="TEntitySearch">EntitySearch</typeparam>
/// <typeparam name="TEntitySearchResult">EntitySearchResult</typeparam>
public abstract class PartialSearchRepository<TContext, TEntity, TKey, TEntitySearch, TEntitySearchResult> :
    AuditableRepository<TContext, TEntity, TKey>, IPartialSearchRepository<TEntity, TKey, TEntitySearch, TEntitySearchResult>
    where TContext : ContextDataBase<TContext>
    where TEntity : class, IEntity<TKey>
    where TKey : IEquatable<TKey>
    where TEntitySearch : EntitySearch
    where TEntitySearchResult : EntityIdLongSearchResult
{
    private const string Id = "Id";

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    protected PartialSearchRepository(TContext context) : base(context)
    {
    }

    /// <inheritdoc/>
    public virtual Task<List<TEntitySearchResult>> Search(TEntitySearch search)
    {
        //Init query (with joins and select)
        IQueryable<TEntitySearchResult> query = InitQuery(search);
        //Where conditions
        query = Where(query, search);
        //Order
        query = OrderBy(query, search, search.OrderBy);
        //Pagination
        query = Paginate(query, search);
        //Results
        return query.ToListAsync();
    }

    /// <inheritdoc/>
    public virtual Task<int> SearchSize(TEntitySearch search)
    {
        IQueryable<TEntitySearchResult> query = InitQuery(search);
        query = Where(query, search);
        return query.CountAsync();
    }

    /// <inheritdoc />
    protected virtual IQueryable<TEntitySearchResult> Where(IQueryable<TEntitySearchResult> querySource, TEntitySearch search)
    {
        IQueryable<TEntitySearchResult> query = querySource;
        ExpressionStarter<TEntitySearchResult> queryExpression = QueryUtils.InitQueryExpression<TEntitySearchResult>();
        AppendConditions(ref queryExpression, search);
        query = query.Where(queryExpression);
        return query;
    }

    /// <summary>
    /// It appends orderby to query
    /// </summary>
    /// <param name="query">Query</param>
    /// <param name="search">Search</param>
    /// <param name="orderColumns">Order Columns</param>
    /// <returns>Query with orderby</returns>
    protected virtual IQueryable<TEntitySearchResultInner> OrderBy<TEntitySearchResultInner, TEntitySearchInner>(IQueryable<TEntitySearchResultInner> query, TEntitySearchInner search, string orderColumns)
            where TEntitySearchResultInner : TEntitySearchResult
            where TEntitySearchInner : TEntitySearch
    {
        return QueryUtils.AppendOrderBy(query, search.OrderCriteria, orderColumns ?? Id);
    }

    /// <summary>
    /// It appends pagination to query
    /// </summary>
    /// <param name="query">Qiuery</param>
    /// <param name="search">Search</param>
    /// <returns>Query with pagination</returns>
    protected virtual IQueryable<TEntitySearchResultInner> Paginate<TEntitySearchInner, TEntitySearchResultInner>(IQueryable<TEntitySearchResultInner> query, TEntitySearchInner search)
        where TEntitySearchInner : TEntitySearch
        where TEntitySearchResultInner : TEntitySearchResult
    {
        return query.Skip(search.FirstResult()).Take(search.PageSize);
    }

    /// <summary>
    /// It inits query
    /// </summary>
    /// <param name="search">Search</param>
    /// <returns>Query</returns>
    protected abstract IQueryable<TEntitySearchResult> InitQuery(TEntitySearch search);

    /// <summary>
    /// It appends conditions to expression query
    /// </summary>
    /// <param name="queryExpression">Query</param>
    /// <param name="search">Search</param>
    /// <returns>Expression query with conditions</returns>
    protected abstract void AppendConditions(ref ExpressionStarter<TEntitySearchResult> queryExpression, TEntitySearch search);


    /// <summary>
    /// This method will append filtering conditions: Any of the defined fields must containg the filter.
    /// </summary>
    /// <param name="queryExpression"></param>
    /// <param name="search"></param>
    /// <param name="fields"></param>
    protected virtual void AppendFilterConditions<TSearchResultInner, TSearchInner>(ref ExpressionStarter<TSearchResultInner> queryExpression, TSearchInner search, List<string> fields)
            where TSearchResultInner : TEntitySearchResult
            where TSearchInner : TEntitySearch
    {
        if (search != null
            && search!.SearchFilter != null
            && search!.SearchFilter!.Trim().Length > 0)
        {
            string filter = search!.SearchFilter!.Trim().ToLower();
            QueryUtils.AppendFilterConditions(ref queryExpression, fields, filter);
        }
    }
}
