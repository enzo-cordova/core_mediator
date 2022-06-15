namespace Genzai.Core.Model.Request;

/// <summary>
/// Base class for requests.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public abstract class Request<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Filtering model class.
    /// </summary>
    public TEntity Model { get; set; }
}