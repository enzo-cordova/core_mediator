namespace Genzai.CosmosDb.Tests.Fixtures;

/// <summary>
/// Cosmos Context fixture.
/// </summary>
public class CosmosContextFixture : IDisposable
{
    private bool disposedValue = false;

    /// <summary>
    /// Get Cosmos config.
    /// </summary>
    public CosmosConfiguration CosmosConfig { get; }

    /// <summary>
    /// Gets cosmos db context.
    /// </summary>
    public CosmosDbContext CosmosContext { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosContextFixture"/> class.
    /// </summary>
    public CosmosContextFixture()
    {
        // Unit Test
        var config = new Mock<CosmosConfiguration>();

        config.SetupProperty(x => x.ConnectionString, It.IsAny<string>());
        config.SetupProperty(x => x.CollectionName, It.IsAny<string>());
        config.SetupProperty(x => x.DatabaseName, It.IsAny<string>());

        this.CosmosConfig = config.Object;

        var client = new Mock<MongoClient>();
        var database = new Mock<IMongoDatabase>();

        client.Setup(x => x.GetDatabase(It.IsAny<string>(), null)).Returns(database.Object);

        var context = new Mock<CosmosDbContext>(client.Object, database.Object);

        this.CosmosContext = context.Object;
    }

    /// <summary>
    /// Dispose method.
    /// </summary>
    public void Dispose()
    {
        this.Dispose(true);
    }

    /// <summary>
    /// Dispose method.
    /// </summary>
    /// <param name="disposing">disposing.</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                this.CosmosContext.Database = null!;
            }

            disposedValue = true;
        }
    }
}