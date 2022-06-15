namespace Genzai.Core.Model.Response;

/// <summary>
/// Response for pagedElements
/// </summary>
[ExcludeFromCodeCoverage]
public abstract class PagedEnumerableResponse<TEntity>
where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedEnumerableResponse{TEntity}"/> class.
    /// </summary>
    /// <param name="pagedResult">Result</param>
    public PagedEnumerableResponse(PagedElements<TEntity> pagedResult)
    {
        PagedResult = pagedResult;
    }

    /// <summary>
    /// Gets or sets pagedResult.
    /// </summary>
    public PagedElements<TEntity> PagedResult { get; }
}