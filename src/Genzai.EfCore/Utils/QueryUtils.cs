using LinqKit;
using System.Reflection;

namespace Genzai.EfCore.Utils;

/// <summary>
/// Utility class for queries
/// </summary>
public static class QueryUtils
{
    /// <summary>
    ///And condition
    /// </summary>
    public const string And = "And";

    /// <summary>
    ///Or condition
    /// </summary>
    public const string Or = "Or";

    /// <summary>
    ///NotAnd  condition
    /// </summary>
    public const string AndNot = "AndNot";

    /// <summary>
    ///NotOr condition
    /// </summary>
    public const string OrNot = "OrNot";

    /// <summary>
    ///Order criteria asc
    /// </summary>
    public const string Asc = "asc";
    /// <summary>
    ///Order criteria desc
    /// </summary>
    public const string Desc = "desc";

    //Search operations
    private const string ContainsOp = "Contains";
    private const string StartsWithOp = "StartsWith";
    private const string EndsWithOp = "EndsWith";
    private const string EqualsOp = "==";
    private const string NotEqualsOp = "!=";
    private const string GreaterThanOp = ">";
    private const string GreaterEqualsThanOp = ">=";
    private const string LessThanOp = "<";
    private const string LessEqualsThanOp = "<=";

    //Other
    private const string ToStringMethod = "toString";
    private const string ToLowerMethod = "toLower";

    private const string Alias = "x";

    /// <summary>
    /// It initializes query expression
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static ExpressionStarter<T> InitQueryExpression<T>()
    {
        return PredicateBuilder.New<T>(true);
    }

    /// <summary>
    /// It appends "contains" condition to query
    /// </summary>
    /// <typeparam name="T">Query type</typeparam>
    /// <param name="query">Query</param>
    /// <param name="propertyName">Condition property</param>
    /// <param name="value">Condition value</param>
    /// <param name="queryOperator">Query operator</param>
    /// <returns>Query with condition</returns>
    public static void AppendConditionStringContains<T>(ref ExpressionStarter<T> query, string propertyName,
        string value, string queryOperator)
    {
        AppendCondition(ref query, propertyName, value, ContainsOp, queryOperator);
    }

    /// <summary>
    /// Add a simple filter by many fields (OR between each field)
    /// with AND operator to the rest of the query.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="query"></param>
    /// <param name="propertyNames"></param>
    /// <param name="filter"></param>
    public static void AppendFilterConditions<T>(ref ExpressionStarter<T> query,
        List<string> propertyNames, string filter)
    {
        ExpressionStarter<T> orFilter = PredicateBuilder.New<T>(true);

        foreach (string prop in propertyNames)
        {
            AppendConditionStringContains(ref orFilter, prop, filter, Or);
        }

        AppendCondition(ref query, orFilter, And);
    }

    /// <summary>
    /// It appends "inList" condition to query
    /// </summary>
    /// <typeparam name="TS">Query Type</typeparam>
    /// <param name="queryExpression">Query</param>
    /// <param name="propertyName">Property</param>
    /// <param name="list">List</param>
    /// <param name="queryOperator">Query operator</param>
    /// <returns>Query with condition</returns>
    public static ExpressionStarter<TS> AppendConditionList<TS>(ref ExpressionStarter<TS> queryExpression, string propertyName,
        ICollection<long?> list, string queryOperator)
    {
        if (list?.Count > 0)
        {
            Expression<Func<TS, bool>> conditionExpression = MakeListContainsExpression<TS>(propertyName, list);
            return AppendCondition(ref queryExpression, conditionExpression, queryOperator);
        }
        return queryExpression;
    }

    /// <summary>
    /// It appends "StartsWith" condition to query
    /// </summary>
    /// <typeparam name="T">Query type</typeparam>
    /// <param name="query">Query</param>
    /// <param name="propertyName">Condition property</param>
    /// <param name="value">Condition value</param>
    /// <param name="queryOperator">Query operator</param>
    /// <returns>Query with condition</returns>
    public static void AppendConditionStringStartsWith<T>(ref ExpressionStarter<T> query, string propertyName,
        string value, string queryOperator)
    {
        AppendCondition(ref query, propertyName, value, StartsWithOp, queryOperator);
    }

    /// <summary>
    /// It appends "EndsWith" condition to query
    /// </summary>
    /// <typeparam name="T">Query type</typeparam>
    /// <param name="query">Query</param>
    /// <param name="propertyName">Condition property</param>
    /// <param name="value">Condition value</param>
    /// <param name="queryOperator">Query Operator</param>
    /// <returns>Query with condition</returns>
    public static void AppendConditionStringEndsWith<T>(ref ExpressionStarter<T> query, string propertyName,
        string value, string queryOperator)
    {
        AppendCondition(ref query, propertyName, value, EndsWithOp, queryOperator);
    }

    /// <summary>
    /// It appends "equals" condition to query
    /// </summary>
    /// <typeparam name="T">Query type</typeparam>
    /// <param name="query">Query</param>
    /// <param name="propertyName">Condition property</param>
    /// <param name="value">value</param>
    /// <param name="queryOperator">Query operator</param>
    /// <returns>Query with condition</returns>
    public static void AppendConditionEquals<T>(ref ExpressionStarter<T> query, string propertyName,
        object value, string queryOperator)
    {
        if (value != null)
        {
            AppendCondition(ref query, BuildPredicate<T>(propertyName, value.ToString(), EqualsOp), queryOperator);
        }
    }

    /// <summary>
    /// It appends "null" condition to query
    /// </summary>
    /// <typeparam name="T">Query type</typeparam>
    /// <param name="query">Query</param>
    /// <param name="propertyName">Condition property</param>
    /// <param name="queryOperator">Query operator</param>
    /// <returns>Query with condition</returns>
    public static ExpressionStarter<T> AppendConditionNull<T>(ref ExpressionStarter<T> query, string propertyName,
        string queryOperator)
    {
        return AppendCondition(ref query, BuildPredicate<T>(propertyName, null, EqualsOp), queryOperator);
    }

    /// <summary>
    /// It appends "null" condition to query
    /// </summary>
    /// <typeparam name="T">Query type</typeparam>
    /// <param name="query">Query</param>
    /// <param name="propertyName">Condition property</param>
    /// <param name="queryOperator">Query operator</param>
    /// <returns>Query with condition</returns>
    public static ExpressionStarter<T> AppendConditionNotNull<T>(ref ExpressionStarter<T> query, string propertyName, string queryOperator)
    {
        return AppendCondition(ref query, BuildPredicate<T>(propertyName, null, NotEqualsOp), queryOperator);
    }

    /// <summary>
    /// It appends "range" condition to query
    /// </summary>
    /// <typeparam name="T">Query type</typeparam>
    /// <param name="query">Query</param>
    /// <param name="propertyName">Condition property</param>
    /// <param name="fromValue">From value</param>
    /// <param name="toValue">To Value</param>
    /// <param name="queryOperator">Query operator</param>
    /// <returns>Query with condition</returns>
    public static void AppendConditionRange<T>(ref ExpressionStarter<T> query, string propertyName, object fromValue,
        object toValue, string queryOperator)
    {
        AppendCondition(ref query, propertyName, fromValue, GreaterEqualsThanOp, queryOperator);
        AppendCondition(ref query, propertyName, toValue, LessEqualsThanOp, queryOperator);
    }

    /// <summary>
    /// It appends condition to query
    /// </summary>
    /// <typeparam name="T">Query type</typeparam>
    /// <param name="query">Query</param>
    /// <param name="propertyName">Condition property</param>
    /// <param name="value">Condition value</param>
    /// <param name="comparison">Comparison</param>
    /// <param name="queryOperator">Query operator</param>
    /// <returns>Query with condition</returns>
    public static void AppendCondition<T>(ref ExpressionStarter<T> query, string propertyName, object value,
        string comparison, string queryOperator)
    {
        if (value != null)
        {
            AppendCondition(ref query, BuildPredicate<T>(propertyName, value.ToString(), comparison), queryOperator);
        }
    }

    /// <summary>
    /// It appends condition to expression
    /// </summary>
    /// <typeparam name="T">Expression type</typeparam>
    /// <param name="query">Query</param>
    /// <param name="expression">Expression</param>
    /// <param name="queryOperator">Operator</param>
    /// <returns>Expresion with condition</returns>
    public static ExpressionStarter<T> AppendCondition<T>(ref ExpressionStarter<T> query,
        Expression<Func<T, bool>> expression, string? queryOperator)
    {
        return queryOperator switch
        {
            Or => query.Or(expression),
            And => query.And(expression),
            OrNot => query.Or(expression.Not()),
            AndNot => query.And(expression.Not()),
            _ => throw new NotSupportedException($"Invalid query operator '{queryOperator}'.")
        };
    }

    /// <summary>
    /// It add "not" to expression
    /// </summary>
    /// <typeparam name="TFunc">Function</typeparam>
    /// <param name="baseExpr">Base expresion</param>
    /// <returns>Expression with "not"</returns>
    public static Expression<TFunc> Not<TFunc>(this Expression<TFunc> baseExpr)
    {
        System.Collections.ObjectModel.ReadOnlyCollection<ParameterExpression> param = baseExpr.Parameters;
        UnaryExpression body = Expression.Not(baseExpr.Body);
        Expression<TFunc> newExpr = Expression.Lambda<TFunc>(body, param);
        return newExpr;
    }

    /// <summary>
    /// It appends orderby to query
    /// </summary>
    /// <typeparam name="TS">Query Type</typeparam>
    /// <param name="query">Query</param>
    /// <param name="orderCriteria">Order criteria: asc or desc</param>
    /// <param name="orderColumn">Column to order</param>
    /// <returns>Query with order</returns>
    public static IQueryable<TS> AppendOrderBy<TS>(IQueryable<TS> query, string orderCriteria, string orderColumn)
    {
        IQueryable<TS>? orderedQuery = null;

        PropertyInfo? property = typeof(TS).GetProperty(orderColumn, BindingFlags.IgnoreCase |
                                                            BindingFlags.Public | BindingFlags.Instance);
        if (orderColumn != null && property != null)
        {
            dynamic lambda = (dynamic)CreateExpression(typeof(TS), property.Name);
            if (orderCriteria?.Equals(Desc) == true)
            {
                orderedQuery = Queryable.OrderByDescending(query, lambda);
            }
            else
            {
                orderedQuery = Queryable.OrderBy(query, lambda);
            }
        }

        return orderedQuery ?? query;
    }


    private static LambdaExpression CreateExpression(Type type, string propertyName)
    {
        ParameterExpression param = Expression.Parameter(type, "x");
        Expression body = param;
        foreach (string member in propertyName.Split('.'))
        {
            body = Expression.PropertyOrField(body, member);
        }
        return Expression.Lambda(body, param);
    }

    /// <summary>
    /// It builds list.contains(x) expression
    /// </summary>
    /// <typeparam name="TS">Type</typeparam>
    /// <param name="propertyName">Property</param>
    /// <param name="list">List</param>
    /// <returns>Expression</returns>
    private static Expression<Func<TS, bool>> MakeListContainsExpression<TS>(string propertyName,
       ICollection<long?> list)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(TS), Alias);
        Expression expression = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
        LambdaExpression lambda = Expression.Lambda(expression, parameter);
        var lambdaExpression = lambda as Expression<Func<TS, long?>>;
        var lambdaExpressionMandatory = lambda as Expression<Func<TS, long>>;
        if (lambdaExpression != null)
        {
            return list.ContainsExpression(lambdaExpression);
        }
        else if (lambdaExpressionMandatory != null)
        {
            return list.Cast<long>().ToList().ContainsExpression(lambdaExpressionMandatory);
        }
        else
        {
            throw new NotSupportedException();
        }
    }

    /// <summary>
    /// Creates lambda expression predicate: (TEntity entity) => collection.Contains(entity.property)
    /// </summary>
    private static Expression<Func<TS, bool>> ContainsExpression<TS, TProperty, TCollection>(
        this TCollection collection,
        Expression<Func<TS, TProperty>> property
    )
        where TCollection : ICollection<TProperty>
    {
        // contains method
        MethodInfo containsMethod = typeof(TCollection).GetMethod(nameof(collection.Contains), new[] { typeof(TProperty) })!;

        // your value
        ConstantExpression collectionInstanceExpression = Expression.Constant(collection);

        // call the contains from a property and apply the value
        MethodCallExpression containsValueExpression = Expression.Call(collectionInstanceExpression, containsMethod, property.Body);

        // create your final lambda Expression
        Expression<Func<TS, bool>> result = Expression.Lambda<Func<TS, bool>>(containsValueExpression, property.Parameters[0]);

        return result;
    }

    /// <summary>
    /// It builds expression
    /// </summary>
    /// <typeparam name="T">Query type</typeparam>
    /// <param name="propertyName">Condition property</param>
    /// <param name="propertyValue">Condition value</param>
    /// <param name="comparison">Comparison</param>
    /// <returns>Expression with condition</returns>
    public static Expression<Func<T, bool>> BuildPredicate<T>(string propertyName, string? propertyValue, string comparison)
    {
        ParameterExpression parameter = Expression.Parameter(typeof(T), Alias);
        Expression left = propertyName.Split('.').Aggregate((Expression)parameter, Expression.Property);
        Expression body = MakeComparison(left, propertyValue, comparison);
        return Expression.Lambda<Func<T, bool>>(body, parameter);
    }

    /// <summary>
    /// It builds expression
    /// </summary>
    /// <param name="left">Left expression</param>
    /// <param name="propertyValue">Property value</param>
    /// <param name="comparison">Comparison</param>
    /// <returns>Expresion</returns>
    /// <exception cref="NotSupportedException"></exception>
    private static Expression MakeComparison(Expression left, string? propertyValue, string comparison)
    {
        return comparison switch
        {
            EqualsOp => MakeBinary(ExpressionType.Equal, left, propertyValue),
            NotEqualsOp => MakeBinary(ExpressionType.NotEqual, left, propertyValue),
            GreaterThanOp => MakeBinary(ExpressionType.GreaterThan, left, propertyValue),
            GreaterEqualsThanOp => MakeBinary(ExpressionType.GreaterThanOrEqual, left, propertyValue),
            LessThanOp => MakeBinary(ExpressionType.LessThan, left, propertyValue),
            LessEqualsThanOp => MakeBinary(ExpressionType.LessThanOrEqual, left, propertyValue),
            ContainsOp or StartsWithOp or EndsWithOp => Expression.Call(MakeString(left), comparison, Type.EmptyTypes, Expression.Constant((propertyValue ?? string.Empty).ToLower(), typeof(string))),
            _ => throw new NotSupportedException($"Invalid comparison operator '{comparison}'."),
        };
    }

    /// <summary>
    /// It builds expresion to invoke toString
    /// </summary>
    /// <param name="source">Expresion</param>
    /// <returns>String expresion</returns>
    private static Expression MakeString(Expression source)
    {
        return MakeToLower(source.Type == typeof(string) ? source : Expression.Call(source, ToStringMethod, Type.EmptyTypes));
    }

    private static Expression MakeToLower(Expression source)
    {
        return Expression.Call(source, ToLowerMethod, Type.EmptyTypes);
    }

    /// <summary>
    /// It builds expression
    /// </summary>
    /// <param name="type">ExpressionType</param>
    /// <param name="left">Left expression</param>
    /// <param name="value">Value</param>
    /// <returns>Expression</returns>
    private static Expression MakeBinary(ExpressionType type, Expression left, string? value)
    {
        object? typedValue = value;
        if (left.Type != typeof(string))
        {
            if (string.IsNullOrEmpty(value))
            {
                typedValue = null;
                if (Nullable.GetUnderlyingType(left.Type) == null)
                {
                    left = Expression.Convert(left, typeof(Nullable<>).MakeGenericType(left.Type));
                }
            }
            else
            {
                Type valueType = Nullable.GetUnderlyingType(left.Type) ?? left.Type;
                if (valueType.IsEnum)
                {
                    typedValue = Enum.Parse(valueType, value);
                }
                else if (valueType == typeof(Guid))
                {
                    typedValue = Guid.Parse(value);
                }
                else
                {
                    typedValue = Convert.ChangeType(value, valueType);
                }
            }
        }
        ConstantExpression right = Expression.Constant(typedValue, left.Type);
        return Expression.MakeBinary(type, left, right);
    }
}
