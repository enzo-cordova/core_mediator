namespace Genzai.Core.Attributes;

/// <summary>
/// Searchable Attributes for domain class
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class SearchableAttribute : Attribute
{
    /// <summary>
    /// Condition searchable
    /// </summary>
    public bool Searchable { get; set; } = true;

    /// <summary>
    /// And-OR
    /// </summary>
    public string Condition { get; set; } = "or";

    /// <summary>
    /// Operation
    /// </summary>
    public string Operation { get; set; } = "Contains";
}