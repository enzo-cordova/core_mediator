namespace Genzai.CosmosDb.Model;

/// <summary>
/// Cosmos response.
/// </summary>
/// <typeparam name="TEntity">Entity Tyep.</typeparam>
public class CosmosResponse<TEntity> : AsyncEnumerableResponse<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosResponse{TEntity}"/> class.
    /// </summary>
    /// <param name="items">Item list.</param>
    /// <param name="continuationToken">Continuation token.</param>
    public CosmosResponse(IAsyncEnumerable<TEntity> items, string continuationToken)
        : base(items)
    {
        this.ContinuationToken = continuationToken;
    }

    /// <summary>
    /// Continuation token.
    /// </summary>
    private string ContinuationToken { get; }
}