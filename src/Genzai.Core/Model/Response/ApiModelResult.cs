namespace Genzai.Core.Model.Response;

/// <summary>
/// Api model result.
/// </summary>
/// <typeparam name="TEntity">Entity Model.</typeparam>
[ExcludeFromCodeCoverage]
public class ApiModelResult<TEntity> : ApiResult
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ApiModelResult{TEntity}"/> class.
    /// </summary>
    /// <param name="resultCode">Result Code</param>
    /// <param name="message">Message</param>
    /// <param name="result">Result</param>
    public ApiModelResult(int resultCode, string message, TEntity result)
        : base(resultCode, message)
    {
        this.Result = result;
    }

    /// <summary>
    /// Gets or sets entity result.
    /// </summary>
    public TEntity Result { get; }
}