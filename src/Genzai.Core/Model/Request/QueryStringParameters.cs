using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace Genzai.Core.Model.Request;

/// <summary>
///  Parameters
/// </summary>
public abstract class QueryStringParameters
{
    /// <summary>
    /// Order by
    /// </summary>
    public string OrderBy { get; set; } = "Id";

    /// <summary>
    /// Order Criteria
    /// </summary>
    [Required]
    [EnumDataType(typeof(OrderingDirections))]
    [JsonConverter(typeof(StringEnumConverter), converterParameters: typeof(CamelCaseNamingStrategy))]
    public OrderingDirections OrderCriteria { get; set; }

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

        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}