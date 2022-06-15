namespace Genzai.CosmosDb.Tests.ExtensionsTest;

/// <summary>
/// Service collection test.
/// </summary>
public class ServiceCollectionExtensionsTest
{
    /// <summary>
    /// Test cosmos service extensions.
    /// </summary>
    [Fact]
    public void Test_Add_CosmosDb_With_Client_Options()
    {
        var services = new ServiceCollection();
        var configuration = new ClientConfiguration
        {
            ConnectionString = "mongodb+srv://serveeeeeer.example.com/",
            CollectionName = "",
            DatabaseName = "",
            Options = new MongoClientSettings { IPv6 = false }
        };

        Action action = () => services.AddCosmosDb(configuration);

        action.Should().Throw<ArgumentException>("Because CosmosDb it's a service.");
    }

    /// <summary>
    /// Test cosmos service extensions.
    /// </summary>
    [Fact]
    public void Test_Add_CosmosDb_Without_Client_Options()
    {
        var services = new ServiceCollection();
        var configuration = new ClientConfiguration
        {
            ConnectionString = "mongodb+srv://serveeeeeer.com",
            CollectionName = "ssdsd",
            DatabaseName = "ssds",
            Options = null
        };

        Action action = () => services.AddCosmosDb(configuration);

        action.Should().Throw<ArgumentException>("Because CosmosDb it's a service.");
    }
}