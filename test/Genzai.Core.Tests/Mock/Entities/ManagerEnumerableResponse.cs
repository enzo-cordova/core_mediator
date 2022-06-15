namespace Genzai.Core.Tests.Mock.Entities;

/// <summary>
/// Manager enumerable response.
/// </summary>
public class ManagerEnumerableResponse : EnumerableResponse<ManagerTest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ManagerEnumerableResponse"/> class.
    /// </summary>
    /// <param name="items">Manager list.</param>
    public ManagerEnumerableResponse(IEnumerable<ManagerTest> items) : base(items)
    {
    }
}