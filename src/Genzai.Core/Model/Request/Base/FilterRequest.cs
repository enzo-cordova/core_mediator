namespace Genzai.Core.Model.Request.Base;

/// <summary>
/// Filter request base.
/// </summary>
/// <typeparam name="TEntity">Entity to Filter.</typeparam>
public abstract class FilterRequest<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Gets or sets filter class.
    /// </summary>
    public TEntity Filter { get; set; }
}