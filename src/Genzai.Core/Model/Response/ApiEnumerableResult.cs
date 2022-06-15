namespace Genzai.Core.Model.Response;

/// <summary>
/// Api List Result.
/// </summary>
/// <typeparam name="TEntity">Entity Type.</typeparam>
[ExcludeFromCodeCoverage]
public class ApiEnumerableResult<TEntity> : ApiResult
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiEnumerableResult{TEntity}"/> class.
    /// </summary>
    /// <param name="resultCode">Result Code</param>
    /// <param name="message">Message</param>
    /// <param name="resultList">Result List</param>
    public ApiEnumerableResult(int resultCode, string message, IEnumerable<TEntity> resultList)
        : base(resultCode, message)
    {
        this.ResultList = resultList;
    }

    /// <summary>
    /// Gets or sets resultList.
    /// </summary>
    public IEnumerable<TEntity> ResultList { get; }
}