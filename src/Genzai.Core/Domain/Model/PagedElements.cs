using Genzai.Core.Extensions;

namespace Genzai.Core.Domain.Model;

/// <summary>
/// Base Class for paging results.
/// </summary>
/// <typeparam name="TEntity">Entity param.</typeparam>
[ExcludeFromCodeCoverage]
public class PagedElements<TEntity>
    where TEntity : class
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PagedElements{TEntity}"/> class.
    /// </summary>
    /// <param name="elements">List of entities.</param>
    /// <param name="totalElements">total elements.</param>
    public PagedElements(IEnumerable<TEntity> elements, int totalElements)
    {
        this.Elements = elements;
        this.TotalElements = totalElements;
    }

    /// <summary>
    /// Gets elements property.
    /// </summary>
    public IEnumerable<TEntity> Elements
    {
        get;
    }

    /// <summary>
    /// Gets total elements property.
    /// </summary>
    public int TotalElements
    {
        get;
    }

    /// <summary>
    /// Default first page with 10 elements
    /// </summary>
    /// <returns></returns>
    public IEnumerable<TEntity> FirstDefaultPage()
    {
        return Elements.Page(1, 10);
    }

    /// <summary>
    /// Any page of this collection
    /// </summary>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public IEnumerable<TEntity> AnyPage(int pageIndex, int pageSize)
    {
        Guard.Against<ArgumentException>(
            pageSize < 0, string.Format(CultureInfo.InvariantCulture, LocalStrings.ParamMoreThanZero, nameof(pageSize)));

        Guard.Against<ArgumentException>(
            pageIndex < 1, string.Format(CultureInfo.InvariantCulture, LocalStrings.ParamMoreThanZero, nameof(pageIndex)));

        return Elements.Page(pageIndex, pageSize);
    }

    /// <summary>
    /// Calculate total pages.
    /// </summary>
    /// <param name="pageSize">Size of page.</param>
    /// <returns>total pages.</returns>
    public int TotalPages(int pageSize)
    {
        Guard.Against<ArgumentException>(
            pageSize < 0, string.Format(CultureInfo.InvariantCulture, LocalStrings.ParamMoreThanZero, nameof(pageSize)));

        return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(this.TotalElements) / pageSize));
    }
}