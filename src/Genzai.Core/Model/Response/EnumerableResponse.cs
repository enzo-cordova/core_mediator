namespace Genzai.Core.Model.Response;

/// <summary>
/// Enumerable response.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
[ExcludeFromCodeCoverage]
public abstract class EnumerableResponse<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AsyncEnumerableResponse{TEntity}"/> class.
    /// </summary>
    /// <param name="items">Item list.</param>
    protected EnumerableResponse(IEnumerable<TEntity> items)
    {
        this.Items = items;
    }

    /// <summary>
    /// List of entities.
    /// </summary>
    [JsonProperty(Required = Required.DisallowNull)]
    public IEnumerable<TEntity> Items { get; }
}