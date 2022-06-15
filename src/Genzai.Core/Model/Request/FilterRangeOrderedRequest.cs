namespace Genzai.Core.Model.Request;

/// <summary>
/// Filter Range ordered request.
/// </summary>
/// <typeparam name="TEntity">Entity to Filter.</typeparam>
public class FilterRangeOrderedRequest<TEntity> : FilterRangeRequest<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Gets or sets orderBy.
    /// </summary>
    /// <value>
    /// OrderBy.
    /// </value>
    public List<OrderBy> OrderBy { get; set; }
}