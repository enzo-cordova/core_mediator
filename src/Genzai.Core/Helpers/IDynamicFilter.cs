namespace Genzai.Core.Helpers;

/// <summary>
/// Interface for dynamic filtering
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IDynamicFilter<T>
    where T : class
{
    /// <summary>
    /// Expression construction
    /// </summary>
    /// <param name="searchFilter"></param>
    /// <param name="numericValues"></param>
    /// <returns></returns>
    Expression<Func<T, bool>> GetFilterExpression(string searchFilter, Dictionary<string, long> numericValues = null);
}