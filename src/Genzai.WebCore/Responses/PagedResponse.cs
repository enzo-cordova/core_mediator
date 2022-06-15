using Newtonsoft.Json;

namespace Genzai.WebCore.Responses;

/// <summary>
/// BasePagedResponse
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedResponse<T> : IEntitySearchResponse
    where T : IEntitySearchResponse
{
    /// <summary>
    /// Items
    /// </summary>
    [JsonProperty(Required = Required.DisallowNull)]
    public IEnumerable<T> Items { get; set; }

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="items">Items</param>
    public PagedResponse(IEnumerable<T> items)
    {
        Items = items;
    }

    /// <summary>
    /// PageNumber
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Page Size
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Search size
    /// </summary>
    public int SearchSize { get; set; }
}
