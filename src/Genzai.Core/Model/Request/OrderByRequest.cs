
namespace Genzai.Core.Model.Request;

/// <summary>
/// Order by class.
/// </summary>
public class OrderByRequest
{
    /// <summary>
    /// Field Name.
    /// </summary>
    public string FieldName { get; set; }

    /// <summary>
    /// Ordering Direction.
    /// </summary>
    public OrderingDirections Direction { get; set; }
}