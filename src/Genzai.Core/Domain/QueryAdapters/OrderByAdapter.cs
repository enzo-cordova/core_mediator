namespace Genzai.Core.Domain.QueryAdapters;

/// <summary>
/// OrderByAdapter class
/// For building lambda expressions for order by.
/// </summary>
/// <typeparam name="TEntity">Entity type.</typeparam>
public class OrderByAdapter<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Lambda parameter.
    /// </summary>
    private const string QueryableTypeName = "param";

    /// <summary>
    /// Lambda order param.
    /// </summary>
    private const string OrderParamName = "orderParam";

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderByAdapter{TEntity}"/> class.
    /// </summary>
    /// <param name="expression">Expression param.</param>
    [ExcludeFromCodeCoverage]
    public OrderByAdapter(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> expression)
    {
        this.InnerExpression = expression;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="OrderByAdapter{TEntity}"/> class.
    /// </summary>
    /// <param name="orderByList">OrderBy list.</param>
    public OrderByAdapter(List<OrderBy> orderByList)
    {
        if (orderByList?.Any() != true)
        {
            this.InnerExpression = order => order.OrderBy(_ => true);
        }
        else
        {
            this.InnerExpression = this.GetEntityOrderBy(orderByList);
        }
    }

    /// <summary>
    /// Gets Expression.
    /// </summary>
    public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> InnerExpression { get; }

    /// <summary>
    /// Get entity orderby.
    /// </summary>
    /// <param name="orderByList">OrderBy list.</param>
    /// <returns>Order Expression.</returns>
    private Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> GetEntityOrderBy(List<OrderBy> orderByList)
    {
        var lambdaMethodName = string.Empty;
        var entityType = typeof(TEntity);
        var queryableType = typeof(IQueryable<TEntity>);
        var queryableParam = Expression.Parameter(queryableType, QueryableTypeName);

        // param => param
        var outerLambda = Expression.Lambda(queryableParam, queryableParam);
        var orderableParam = Expression.Parameter(entityType, OrderParamName);

        Expression result = outerLambda.Body;

        foreach (var element in orderByList)
        {
            Expression memberAccess = null;
            LambdaExpression orderByLambda = null;

            if (string.IsNullOrEmpty(lambdaMethodName))
            {
                lambdaMethodName = GetOrderByMethod(element.Direction);
            }
            else
            {
                lambdaMethodName = GetThenOrderByMethod(element.Direction);
            }

            var propertyInfo = entityType.GetProperty(
                element.FieldName,
                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            // param.propertyInfo
            memberAccess = Expression.Property(orderableParam, propertyInfo);

            // param => param.propertyinfo
            orderByLambda = Expression.Lambda(memberAccess, orderableParam);

            // OrderBy(param => param.propertyInfo)
            result = Expression.Call(
                typeof(Queryable),
                lambdaMethodName,
                new[] { entityType, propertyInfo.PropertyType },
                result,
                Expression.Quote(orderByLambda));
        }

        var finalLambda = Expression.Lambda<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>(result, queryableParam);

        return finalLambda.Compile();
    }

    /// <summary>
    /// Get order by method.
    /// </summary>
    /// <param name="direction">Direction -1 Desc : 1 Asc.</param>
    /// <returns>Lambda Method string.</returns>
    private static string GetOrderByMethod(int direction)
    {
        var stringDirection = direction == 1 ? string.Empty : "Descending";

        return $"OrderBy{stringDirection}";
    }

    /// <summary>
    /// Get then by method.
    /// </summary>
    /// <param name="direction">Direction -1 Desc : 1 Asc.</param>
    /// <returns>Lambda Method string.</returns>
    private static string GetThenOrderByMethod(int direction)
    {
        var stringDirection = direction == 1 ? string.Empty : "Descending";

        return $"ThenBy{stringDirection}";
    }
}