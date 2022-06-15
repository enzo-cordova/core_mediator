using Genzai.Auxiliar.Client.Infrastructure.Data.Services;
using Genzai.Core.Helpers;

namespace Genzai.Core.Tests.Helpers;

/// <summary>
/// Test for dynmic filter
/// </summary>
public class DynamicFilterTest
{
    private readonly IDynamicFilter<SearchableClassTest> dynamicFilter;

    public DynamicFilterTest()
    {
        dynamicFilter = new DynamicFilter<SearchableClassTest>();
    }

    /// <summary>
    /// Test OK Result
    /// </summary>
    [Fact]
    public void Test()
    {
        string searchFilter = "asd";

        var expression = dynamicFilter.GetFilterExpression(searchFilter);

        expression.Should().NotBe(null);
    }
}