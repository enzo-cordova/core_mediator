namespace Genzai.Core.Model.Request;

/// <summary>
/// FilterRequest
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class FilterRangeRequest<TEntity> : FilterRequest<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Gets or sets start.
    /// </summary>
    /// <value>
    /// Start.
    /// </value>
    public int Start { get; set; }

    /// <summary>
    /// Gets or sets size.
    /// </summary>
    /// <value>
    /// Size.
    /// </value>
    public int Size { get; set; }
}