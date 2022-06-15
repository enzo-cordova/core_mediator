namespace Genzai.Core.Domain.QueryAdapters;

/// <summary>
/// FilterAdapter class
/// For building filters.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public class FilterAdapter<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="FilterAdapter{TEntity}"/> class.
    /// </summary>
    /// <param name="expression">Expression filter.</param>
    public FilterAdapter(Expression<Func<TEntity, bool>> expression)
    {
        this.InnerExpression = expression;
    }

    /// <summary>
    /// Gets Expression filter.
    /// </summary>
    public Expression<Func<TEntity, bool>> InnerExpression { get; }
}