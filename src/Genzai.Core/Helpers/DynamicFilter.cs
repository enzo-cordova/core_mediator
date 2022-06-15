using Genzai.Core.Attributes;
using Genzai.Core.Helpers;

namespace Genzai.Auxiliar.Client.Infrastructure.Data.Services;

/// <summary>
/// Implementation
/// </summary>
/// <typeparam name="T"></typeparam>
[ExcludeFromCodeCoverage]
public class DynamicFilter<T> : IDynamicFilter<T>
    where T : class
{
    [ExcludeFromCodeCoverage]
    private MethodCallExpression GetStringExpression(CustomAttributeNamedArgument operationArgument, string searchFilter, MemberExpression nameMember)
    {
        MethodInfo method = typeof(string).GetMethod(operationArgument.TypedValue.Value.ToString(), new[] { typeof(string), typeof(StringComparison) });
        // MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string)});

        var someValue = Expression.Constant(searchFilter, typeof(string));
        var compare = Expression.Constant(StringComparison.CurrentCultureIgnoreCase, typeof(StringComparison));

        var named = Expression.Call(nameMember, method, someValue, compare);

        return named;
    }

    [ExcludeFromCodeCoverage]
    private MethodCallExpression GetNumberExpression(CustomAttributeNamedArgument operationArgument, long value, MemberExpression nameMember)
    {
        MethodInfo method = typeof(long).GetMethod(operationArgument.TypedValue.Value.ToString(), new[] { typeof(long) });

        var someValue = Expression.Constant(value, typeof(long));
        var named = Expression.Call(nameMember, method, someValue);

        return named;
    }

    [ExcludeFromCodeCoverage]
    private void GetFinalStringExpression(ref Expression finalExpression,
                                        CustomAttributeNamedArgument operationArgument,
                                        string searchFilter,
                                        MemberExpression nameMember,
                                        CustomAttributeNamedArgument conditionArgument)
    {
        if (finalExpression == null)
        {
            finalExpression = GetStringExpression(operationArgument, searchFilter, nameMember);
            return;
        }

        switch (conditionArgument.TypedValue.Value.ToString().ToLowerInvariant())
        {
            case "or":
                finalExpression = Expression.OrElse(finalExpression, GetStringExpression(operationArgument, searchFilter, nameMember));
                break;

            case "and":
                finalExpression = Expression.AndAlso(finalExpression, GetStringExpression(operationArgument, searchFilter, nameMember));
                break;
        }
    }

    [ExcludeFromCodeCoverage]
    private void GetFinalLongExpression(ref Expression finalExpression,
                                        CustomAttributeNamedArgument operationArgument,
                                        long value,
                                        MemberExpression nameMember,
                                        CustomAttributeNamedArgument conditionArgument)
    {
        if (finalExpression == null)
        {
            finalExpression = GetNumberExpression(operationArgument, value, nameMember);
            return;
        }

        switch (conditionArgument.TypedValue.Value.ToString().ToLowerInvariant())
        {
            case "or":
                finalExpression = Expression.OrElse(finalExpression, GetNumberExpression(operationArgument, value, nameMember));
                break;

            case "and":
                finalExpression = Expression.AndAlso(finalExpression, GetNumberExpression(operationArgument, value, nameMember));
                break;
        }
    }

    /// <summary>
    /// Expression filter for customer overview
    /// </summary>
    /// <param name="searchFilter"></param>
    /// <param name="numericValues"></param>
    /// <returns>Expression result</returns>
    public Expression<Func<T, bool>> GetFilterExpression(string searchFilter, Dictionary<string, long> numericValues = null)
    {
        Expression finalExpression = null;
        var queryParam = Expression.Parameter(typeof(T), "param");

        if (string.IsNullOrEmpty(searchFilter))
            return Expression.Lambda<Func<T, bool>>(finalExpression ?? Expression.Constant(true), queryParam);

        var properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttributes(typeof(SearchableAttribute), false).Count() == 1);

        var stringPropierties = properties.Where(c => c.PropertyType == typeof(string));
        var numericProperties = properties.Where(c => c.PropertyType == typeof(long));

        foreach (var propertyInfo in stringPropierties)
        {
            var nameMember = Expression.Property(queryParam, propertyInfo.Name);
            var operationArgument = propertyInfo.CustomAttributes.SelectMany(c => c.NamedArguments)
                .FirstOrDefault(c => c.MemberName == "Operation");

            var conditionArgument = propertyInfo.CustomAttributes.SelectMany(c => c.NamedArguments)
                .FirstOrDefault(c => c.MemberName == "Condition");

            GetFinalStringExpression(ref finalExpression, operationArgument, searchFilter, nameMember,
                conditionArgument);
        }

        foreach (var propertyInfo in numericProperties)
        {
            if (numericValues != null && numericValues.ContainsKey(propertyInfo.Name))
            {
                var nameMember = Expression.Property(queryParam, propertyInfo.Name);
                var operationArgument = propertyInfo.CustomAttributes.SelectMany(c => c.NamedArguments)
                    .FirstOrDefault(c => c.MemberName == "Operation");

                var conditionArgument = propertyInfo.CustomAttributes.SelectMany(c => c.NamedArguments)
                    .FirstOrDefault(c => c.MemberName == "Condition");

                GetFinalLongExpression(ref finalExpression, operationArgument, numericValues[propertyInfo.Name], nameMember, conditionArgument);
            }
        }

        return Expression.Lambda<Func<T, bool>>(finalExpression ?? Expression.Constant(true), queryParam);
    }
}