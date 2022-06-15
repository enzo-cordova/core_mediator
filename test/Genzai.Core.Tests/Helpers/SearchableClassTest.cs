using Genzai.Core.Attributes;

namespace Genzai.Core.Tests.Helpers;

public class SearchableClassTest
{
    [Searchable(Operation = "Contains", Condition = "or", Searchable = true)]
    public string Field1 { get; set; }

    [Searchable(Operation = "Contains", Condition = "or", Searchable = true)]
    public string Filed2 { get; set; }

    [Searchable(Operation = "Contains", Condition = "or", Searchable = true)]
    public long Field3 { get; set; }
}