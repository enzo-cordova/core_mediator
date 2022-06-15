namespace Genzai.Core.Model.Response;

/// <summary>
/// Async enumerable response.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
[ExcludeFromCodeCoverage]
public abstract class AsyncEnumerableResponse<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncEnumerableResponse{TEntity}"/> class.
    /// </summary>
    /// <param name="items">Item list.</param>
    protected AsyncEnumerableResponse(IAsyncEnumerable<TEntity> items)
    {
        this.Items = items;
    }

    /// <summary>
    /// List of entities.
    /// </summary>
    public IAsyncEnumerable<TEntity> Items { get; }
}