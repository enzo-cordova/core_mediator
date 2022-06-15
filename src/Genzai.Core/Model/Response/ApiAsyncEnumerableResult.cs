namespace Genzai.Core.Model.Response;

/// <summary>
/// Api Async enumerable result
/// </summary>
/// <typeparam name="TEntity">Entity Type</typeparam>
[ExcludeFromCodeCoverage]
public class ApiAsyncEnumerableResult<TEntity> : ApiResult
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiEnumerableResult{TEntity}"/> class.
    /// </summary>
    /// <param name="resultCode">Result Code</param>
    /// <param name="message">Message</param>
    /// <param name="resultList">Result List</param>
    public ApiAsyncEnumerableResult(int resultCode, string message, IAsyncEnumerable<TEntity> resultList)
        : base(resultCode, message)
    {
        this.Items = resultList;
    }

    /// <summary>
    /// List of entities.
    /// </summary>
    public IAsyncEnumerable<TEntity> Items { get; }
}