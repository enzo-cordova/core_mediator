namespace Genzai.CosmosDb.Tests.ContextTest;

/// <summary>
/// Cosmos context test.
/// </summary>
public class CosmosDbContextTest : IClassFixture<CosmosContextFixture>
{
    private readonly CosmosContextFixture _contextFixture;

    /// <summary>
    /// Initializes a new instance of the <see cref="CosmosDbContextTest"/> class.
    /// </summary>
    /// <param name="contextFixture">Context fixture.</param>
    public CosmosDbContextTest(CosmosContextFixture contextFixture)
    {
        this._contextFixture = contextFixture;
    }

    /// <summary>
    /// Test create data base in Mongo.
    /// </summary>
    [Fact]
    public void Test_Create_Database()
    {
        var mockdbsettings = new Mock<MongoDatabaseSettings>();

        Func<Task> action = async () => await _contextFixture.CosmosContext.EnsureDatabaseCreatedAsync(
            "test", mockdbsettings.Object);

        action.Should().NotThrowAsync<ArgumentNullException>();
    }

    /// <summary>
    /// Test create data base in Mongo Get Error.
    /// </summary>
    [Fact]
    public void Test_Create_Database_Get_Error()
    {
        Func<Task> action = async () => await _contextFixture.CosmosContext.EnsureDatabaseCreatedAsync(
            It.IsAny<string>(), It.IsAny<MongoDatabaseSettings>());

        action.Should().ThrowAsync<ArgumentNullException>();
    }

    /// <summary>
    /// Create New Collection
    /// </summary>
    [Fact]
    public void Test_Create_New_Collection()
    {
        var collectionOpt = new Mock<CreateCollectionOptions>();
        var mongoDBOpt = new Mock<MongoDatabaseSettings>();

        Action action = () => _contextFixture.CosmosContext.CreateMongoCollectionAsync("test", "test", mongoDBOpt.Object, collectionOpt.Object, It.IsAny<CancellationToken>());

        action.Should().NotThrow<Exception>();
    }

    /// <summary>
    /// Create New Collection get error
    /// </summary>
    [Fact]
    public async void Test_Create_New_Collection_NullDB_Error()
    {
        var collectionOpt = new Mock<CreateCollectionOptions>();
        var mongoDBOpt = new Mock<MongoDatabaseSettings>();
        var collectionName = "";
        IMongoDatabase db = null;
        var clientMock = new Mock<MongoClient>();

        var sut = new CosmosDbContext(clientMock.Object);

        sut.Database = db;

        Func<Task> action = async () => await sut.CreateMongoCollectionAsync("", "test", null, null);

        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Get created container.
    /// </summary>
    [Fact]
    public void Test_Get_Created_Collection()
    {
        Action action = () => _contextFixture.CosmosContext.GetCollectionByName<Client>("ds");

        action.Should().NotThrow<ArgumentNullException>();
    }

    /// <summary>
    /// Get created Collection Exception.
    /// </summary>
    [Fact]
    public void Test_Get_Created_Collection_Error()
    {
        Action action = () => _contextFixture.CosmosContext.GetCollectionByName<Client>(null);

        action.Should().Throw<ArgumentNullException>();
    }

    /// <summary>
    /// Test for delete database in cosmos.
    /// </summary>
    [Fact]
    public void Test_Delete_Database()
    {
        var clientMock = new Mock<MongoClient>();

        //clientMock.Setup(c=>c.DropDatabaseAsync(It.IsAny<string>(),It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);
        Action action = () => _contextFixture.CosmosContext.DeleteDatabaseAsync(It.IsAny<CancellationToken>());

        action.Should().NotThrow();
    }

    /// <summary>
    /// Create cosmos context test.
    /// </summary>
    [Fact]
    public void Test_Create_Mongo_Context()
    {
        //Arrange
        var client = new Mock<MongoClient>();
        var database = new Mock<IMongoDatabase>();

        //Act
        var cosmosContext = new CosmosDbContext(client.Object, database.Object);

        //Assert
        cosmosContext.Should().NotBeNull();
    }

    /// <summary>
    /// Create cosmos context test.
    /// </summary>
    [Fact]
    public void Test_Create_Mongo_Context_Single_Constructor_Param()
    {
        //Arrange
        var client = new Mock<MongoClient>();

        //Act
        var mongoContext = new CosmosDbContext(client.Object);

        //Assert
        mongoContext.Should().NotBeNull();
    }

    /// <summary>
    /// Get collection by name
    /// </summary>
    [Fact]
    public void Test_Get_Mongo_Collection_By_Name()
    {
        //Arrange
        var mongoEntityDomainMock = new Mock<CosmosEntityDomain>();

        var db = new Mock<IMongoDatabase>();
        //object p = db.Setup(d => d.GetCollection<BsonDocument>("test")).Returns(It.IsAny<IMongoCollection<CosmosEntityDomain>>());
        //Act

        Action action = () => _contextFixture.CosmosContext.GetCollectionByName<Client>("test");

        //Assert
        action.Should().NotThrow<Exception>();
    }
}