namespace Genzai.Core.Model.Response;

/// <summary>
/// Base model response.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
[ExcludeFromCodeCoverage]
public abstract class ModelResponse<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ModelResponse{TEntity}"/> class.
    /// </summary>
    /// <param name="Item">Item instance.</param>
    protected ModelResponse(TEntity Item)
    {
        this.Item = Item;
    }

    /// <summary>
    /// Entity model.
    /// </summary>
    public TEntity Item { get; }
}