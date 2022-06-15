using System.Linq.Dynamic.Core;

namespace Genzai.Core.Helpers;

/// <summary>
/// Interface implementation
/// </summary>
/// <typeparam name="T"></typeparam>
[ExcludeFromCodeCoverage]
public class SortHelper<T> : ISortHelper<T>
where T : class
{
    /// <summary>
    /// ApplySort
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="orderByQueryString"></param>
    /// <returns></returns>
    public IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString)
    {
        if (!entities.Any())
            return entities;

        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return entities;
        }

        var orderParams = orderByQueryString.Trim().Split(',');
        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var orderQueryBuilder = new StringBuilder();

        foreach (var param in orderParams)
        {
            if (string.IsNullOrWhiteSpace(param))
                continue;

            var propertyFromQueryName = param.Split(" ")[0];
            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
                continue;

            var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";

            orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
        }

        var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

        if (string.IsNullOrWhiteSpace(orderQuery))
            return entities;

        return entities.OrderBy(orderQuery);
    }


    /// <summary>
    /// Method for implementations
    /// </summary>
    /// <param name="orderByQueryString"></param>
    /// <param name="orderCriteriaString"></param>
    /// <returns></returns>
    public string ApplySort(string orderByQueryString, string orderCriteriaString)
    {
        if (string.IsNullOrWhiteSpace(orderByQueryString))
        {
            return string.Empty;
        }

        var orderParams = orderByQueryString.Trim().Split(',');
        var criteriaParams = orderCriteriaString.Trim().Split(',');
        var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

        var orderQueryBuilder = new StringBuilder();
        for (int i = 0; i < orderParams.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(orderParams[i]))
                continue;

            var propertyFromQueryName = orderParams[i].Split(" ")[0];
            var criteriaFromQueryName = criteriaParams[i].Split(" ")[0];

            var objectProperty = propertyInfos.FirstOrDefault(pi => pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

            if (objectProperty == null)
                continue;

            var sortingOrder = criteriaFromQueryName.Contains("desc") ? "descending" : "ascending";

            orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
        }
        return orderQueryBuilder.ToString().TrimEnd(',', ' ');
    }
}