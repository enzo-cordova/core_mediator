namespace Genzai.Core.Domain.QueryAdapters;

/// <summary>
/// ExpressionAdapter class
/// For building lambda expressions for Includes.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public class IncludesAdapter<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IncludesAdapter{TEntity}"/> class.
    /// </summary>
    /// <param name="expression">Expression param.</param>
    public IncludesAdapter(Func<IQueryable<TEntity>, IQueryable<TEntity>> expression)
    {
        this.Expression = expression;
    }

    /// <summary>
    /// Gets expression func.
    /// </summary>
    public Func<IQueryable<TEntity>, IQueryable<TEntity>> Expression { get; }
}