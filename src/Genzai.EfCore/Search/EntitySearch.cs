namespace Genzai.EfCore.Search;

/// <summary>
/// Entity search
/// </summary>
public abstract class EntitySearch
{
    /// <summary>
    /// Search filter
    /// </summary>
    public string? SearchFilter { get; set; }

    /// <summary>
    /// Order by
    /// </summary>
    public string OrderBy { get; set; } = "Id";

    /// <summary>
    /// Order Criteria
    /// </summary>
    public string OrderCriteria { get; set; } = "asc";

    /// <summary>
    /// Order by default 50
    /// </summary>
    private const int MaxPageSize = 50;

    /// <summary>
    /// Page Number
    /// </summary>
    public int PageNumber { get; set; } = 1;

    private int _pageSize = 10;

    /// <summary>
    /// Page Size default 10
    /// </summary>
    public int PageSize
    {
        get => _pageSize;

        set => _pageSize = value > MaxPageSize ? MaxPageSize : value;
    }

    /// <summary>
    /// Devuelve el primer resultado de la paginación.
    /// </summary>
    /// <returns>El primer resultado de los elementos buscados.</returns>
    public int FirstResult()
    {
        int pageNumber = PageNumber;
        if (pageNumber >= 1)
        {
            return (pageNumber - 1) * PageSize;
        }
        else
        {
            return 0;
        }
    }
}
