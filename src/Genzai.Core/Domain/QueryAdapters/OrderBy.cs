namespace Genzai.Core.Domain.QueryAdapters;

/// <summary>
/// Order by class.
/// </summary>
public class OrderBy
{
    /// <summary>
    /// Gets or sets field Name.
    /// </summary>
    public string FieldName { get; set; }

    /// <summary>
    /// Gets or sets direction.
    /// </summary>
    /// <example>-1 Desc : 1 Asc.</example>
    public int Direction { get; set; }
}