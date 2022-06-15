namespace Genzai.Core.Model.Request;

/// <summary>
/// Request class for pagination request.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public abstract class PagedRequest<TEntity> : Request<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Start point.
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Fetch Size.
    /// </summary>
    public int PageSize { get; set; }
}