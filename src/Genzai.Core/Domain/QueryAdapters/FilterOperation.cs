namespace Genzai.Core.Domain.QueryAdapters;

/// <summary>
/// Filter operation.
/// </summary>
public enum FilterOperation
{
    /// <summary>
    /// Equals
    /// </summary>
    Equals = 0,

    /// <summary>
    /// Not Equals
    /// </summary>
    NotEquals = 1,

    /// <summary>
    /// Contains
    /// </summary>
    Contains = 2,

    /// <summary>
    /// Starts With
    /// </summary>
    StartsWith = 3,

    /// <summary>
    /// Ends With
    /// </summary>
    EndsWith = 4,

    /// <summary>
    /// Greater than
    /// </summary>
    GreaterThan = 5,

    /// <summary>
    /// Greater than or equals
    /// </summary>
    GreaterThanOrEqual = 6,

    /// <summary>
    /// Less than
    /// </summary>
    LessThan = 7,

    /// <summary>
    /// Less than or Equals
    /// </summary>
    LessThanOrEqual = 8,

    /// <summary>
    /// Contains for string expresssions
    /// </summary>
    ConstaisString = 9
}