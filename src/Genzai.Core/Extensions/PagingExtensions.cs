namespace Genzai.Core.Extensions;

/// <summary>
/// Paging extensions for colections.
/// </summary>
public static class PagingExtensions
{
    /// <summary>
    /// IQueryable page extension.
    /// </summary>
    /// <typeparam name="T">Object Type.</typeparam>
    /// <param name="query">Query.</param>
    /// <param name="pageIndex">Page index.</param>
    /// <param name="pageSize">Page size.</param>
    /// <returns>Paged query.</returns>
    public static IQueryable<T> Page<T>(this IQueryable<T> query, int pageIndex, int pageSize)
    {
        Guard.Against<ArgumentException>(
            pageIndex < 0, string.Format(CultureInfo.InvariantCulture, LocalStrings.ParamEqualOrMoreThanZero, nameof(pageIndex)));
        Guard.Against<ArgumentException>(
            pageSize <= 0, string.Format(CultureInfo.InvariantCulture, LocalStrings.ParamMoreThanZero, nameof(pageSize)));

        var skip = (pageIndex - 1) * pageSize;

        return query.Skip(skip).Take(pageSize);
    }

    /// <summary>
    /// IEnumerable page extension.
    /// </summary>
    /// <typeparam name="T">Object type.</typeparam>
    /// <param name="query">IEnumerable.</param>
    /// <param name="pageIndex">Page index.</param>
    /// <param name="pageSize">Page size.</param>
    /// <returns>Paged enumerable.</returns>
    public static IEnumerable<T> Page<T>(this IEnumerable<T> query, int pageIndex, int pageSize)
    {
        Guard.Against<ArgumentException>(
            pageIndex < 0, string.Format(CultureInfo.InvariantCulture, LocalStrings.ParamEqualOrMoreThanZero, nameof(pageIndex)));
        Guard.Against<ArgumentException>(
            pageSize <= 0, string.Format(CultureInfo.InvariantCulture, LocalStrings.ParamMoreThanZero, nameof(pageSize)));

        var skip = (pageIndex - 1) * pageSize;

        return query.Skip(skip).Take(pageSize);
    }
}