namespace Genzai.Core.Domain.QueryAdapters;

/// <summary>
/// FilterAdapter class
/// For building filters.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TModel">Model type</typeparam>
public class NewFilterAdapter<TEntity, TModel>
    where TEntity : class
    where TModel : class
{
    private const string ParamName = "param";

    /// <summary>
    /// Final Expression
    /// </summary>
    protected Expression FinalExpression { get; private set; }

    /// <summary>
    /// Filter model
    /// </summary>
    protected TModel FilterModel { get; }

    /// <summary>
    /// Query Parameter
    /// </summary>
    protected ParameterExpression QueryParameter { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NewFilterAdapter{TEntity, TModel}"/> class.
    /// </summary>
    /// <param name="filterModel">Filter Model</param>
    public NewFilterAdapter(TModel filterModel)
    {
        this.FilterModel = filterModel;
        this.FinalExpression = null;
        this.QueryParameter = Expression.Parameter(typeof(TEntity), ParamName);
    }

    /// <summary>
    /// Add and Also exression
    /// </summary>
    /// <param name="expression">Expression to be added</param>
    protected void AddAndAlso(Expression expression)
    {
        if (this.FinalExpression == null)
        {
            this.FinalExpression = expression;
        }
        else
        {
            // && param.Equals(value)
            this.FinalExpression = Expression.AndAlso(this.FinalExpression, expression);
        }
    }

    /// <summary>
    /// Add or else
    /// </summary>
    /// <param name="expression">Expression to be added</param>
    protected void AddOrElse(Expression expression)
    {
        if (this.FinalExpression == null)
        {
            this.FinalExpression = expression;
        }
        else
        {
            // || param.Equals(value)
            this.FinalExpression = Expression.OrElse(this.FinalExpression, expression);
        }
    }

    /// <summary>
    /// Get expression lambda
    /// </summary>
    /// <param name="parameterName">Paramater name</param>
    /// <param name="parameterValue">Parameter value</param>
    /// <param name="filterOperation">Filter operation</param>
    /// <returns>Lambda Expression</returns>
    protected Expression GetExpression(string parameterName, object parameterValue, FilterOperation filterOperation)
    {
        // param
        var memberAccess = Expression.Property(this.QueryParameter, parameterName);

        switch (filterOperation)
        {
            case FilterOperation.Equals:
                return Expression.Equal(memberAccess, Expression.Constant(parameterValue));

            case FilterOperation.NotEquals:
                return Expression.NotEqual(memberAccess, Expression.Constant(parameterValue));

            case FilterOperation.Contains:
                if (parameterName.GetType() == typeof(List<int>))
                {
                    return Expression.Call(memberAccess, typeof(List<int>).GetMethod("Contains"), Expression.Constant(parameterValue));
                }

                if (parameterName.GetType() == typeof(List<string>))
                {
                    return Expression.Call(memberAccess, typeof(List<string>).GetMethod("Contains"), Expression.Constant(parameterValue));
                }

                return Expression.Call(
                    memberAccess, typeof(string).GetMethod("Contains"), Expression.Constant(parameterValue));

            case FilterOperation.StartsWith:
                return Expression.Call(
                    memberAccess, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), Expression.Constant(parameterValue));

            case FilterOperation.EndsWith:
                return Expression.Call(
                    memberAccess, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), Expression.Constant(parameterValue));

            case FilterOperation.GreaterThan:
                return Expression.GreaterThan(memberAccess, Expression.Constant(parameterValue));

            case FilterOperation.GreaterThanOrEqual:
                return Expression.GreaterThanOrEqual(memberAccess, Expression.Constant(parameterValue));

            case FilterOperation.LessThan:
                return Expression.LessThan(memberAccess, Expression.Constant(parameterValue));

            case FilterOperation.LessThanOrEqual:
                return Expression.LessThanOrEqual(memberAccess, Expression.Constant(parameterValue));

            case FilterOperation.ConstaisString:
                MethodInfo refmethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                return Expression.Call(memberAccess, refmethod, Expression.Constant(parameterValue));
        }

        return null;
    }

    /// <summary>
    /// Get final lambda.
    /// </summary>
    /// <returns>Filter satisfied</returns>
    protected Expression<Func<TEntity, bool>> GetFinalLambda()
    {
        return Expression.Lambda<Func<TEntity, bool>>(this.FinalExpression ?? Expression.Constant(true), this.QueryParameter);
    }
}