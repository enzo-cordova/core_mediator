namespace Genzai.Core.Model.Response;

/// <summary>
/// Api paged result.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
[ExcludeFromCodeCoverage]
public class ApiPagedEnumerableResult<TEntity> : ApiResult
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiPagedEnumerableResult{TEntity}"/> class.
    /// </summary>
    /// <param name="resultCode">Result Code</param>
    /// <param name="message">Message</param>
    /// <param name="pagedResult">Result</param>
    public ApiPagedEnumerableResult(int resultCode, string message, PagedElements<TEntity> pagedResult)
        : base(resultCode, message)
    {
        this.PagedResult = pagedResult;
    }

    /// <summary>
    /// Gets or sets pagedResult.
    /// </summary>
    public PagedElements<TEntity> PagedResult { get; }
}