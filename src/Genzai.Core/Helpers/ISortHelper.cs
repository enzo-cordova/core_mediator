namespace Genzai.Core.Helpers;

/// <summary>
/// Interface for sorting expressions
/// </summary>
/// <typeparam name="T"></typeparam>
public interface ISortHelper<T>
{
    /// <summary>
    /// Sort query to be used
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="orderByQueryString"></param>
    /// <returns></returns>
    IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString);

    /// <summary>
    /// To sort method
    /// </summary>
    /// <param name="orderByQueryString"></param>
    /// <param name="orderCriteriaString"></param>
    /// <returns></returns>
    string ApplySort(string orderByQueryString, string orderCriteriaString);
}