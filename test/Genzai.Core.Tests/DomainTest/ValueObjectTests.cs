namespace Genzai.Core.Tests.DomainTest;

/// <summary>
/// Value object test.
/// </summary>
public class ValueObjectTests
{
    /// <summary>
    /// Test Value Object Address Equals
    /// </summary>
    [Fact]
    public void Test_Value_Object_Address_Equals()
    {
        var arrange = ObjectsMocks.GetOrders(10).ToList();
        var addressOne = arrange[0].Address;
        var addressTwo = arrange[1].Address;

        var act = addressOne.Equals(addressTwo);

        act.Should().BeFalse();
    }

    /// <summary>
    /// Test Value Object Atomic Values
    /// </summary>
    [Fact]
    public void Test_Value_Object_Atomic_Values()
    {
        var arrange = ObjectsMocks.GetOrders(10).ToList();
        var addressOne = arrange[0].Address;

        var act = addressOne.PropertyList;

        act.Should().NotBeEmpty();
    }
}