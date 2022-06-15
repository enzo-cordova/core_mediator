namespace Genzai.EfCore.Tests.EntityTest;

/// <summary>
/// Entity tests.
/// </summary>
public class EntityTests
{
    /// <summary>
    /// Test entity map configuration is null.
    /// </summary>
    [Fact]
    public void Test_Entity_Map_Configuration_Is_Null()
    {
        var entityMap = new Faker<ProductMap>().Generate();

        Action action = () => entityMap.Configure(null);

        action.Should().Throw<ArgumentNullException>();
    }
}