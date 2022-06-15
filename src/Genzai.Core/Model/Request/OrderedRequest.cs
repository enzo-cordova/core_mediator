namespace Genzai.Core.Model.Request;

/// <summary>
/// Ordered Request.
/// </summary>
/// <typeparam name="TEntity">Entity Type.</typeparam>
public abstract class OrderedRequest<TEntity> : PagedRequest<TEntity>
    where TEntity : class
{
    /// <summary>
    /// List of ordering fields and directions.
    /// </summary>
    public List<OrderBy> OrderingList { get; set; }
}