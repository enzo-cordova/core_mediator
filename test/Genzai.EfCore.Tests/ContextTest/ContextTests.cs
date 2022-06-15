namespace Genzai.EfCore.Tests.ContextTest;

/// <summary>
/// Context Test.
/// </summary>
public class ContextTests : IClassFixture<DatabaseFixture>
{
    private readonly DatabaseFixture fixture;

    /// <summary>
    /// Initializes a new instance of the <see cref="ContextTests"/> class.
    /// </summary>
    /// <param name="fixture">Context fixture.</param>
    public ContextTests(DatabaseFixture fixture)
    {
        this.fixture = fixture;
    }

    /// <summary>
    /// Test execute sql script.
    /// </summary>
    [Fact]
    public void Test_Execute_Sql_Script()
    {
        var elementToQuery = this.fixture.Context.Orders.FirstOrDefault();

        Action action = () => this.fixture.Context.ExecuteSqlCommand("Select * from Orders where Id={0}", elementToQuery.Id);

        // In Memory database, sql scripts is not working.
        action.Should().Throw<InvalidOperationException>();
    }

    /// <summary>
    /// Test execute sql script.
    /// </summary>
    [Fact]
    public void Test_Execute_Sql_Script_Alternative()
    {
        var elementToQuery = this.fixture.Context.Orders.FirstOrDefault();

        Func<Task> func = async () => await this.fixture.Context.ExecuteSqlCommandAsync("Select * from Orders where Id={0}", new List<object>() { elementToQuery.Id });

        // In Memory database, sql scripts is not working.
        func.Should().Throw<InvalidOperationException>();
    }

    /// <summary>
    /// Test begin transaction.
    /// </summary>
    [Fact]
    public void Test_Begin_Transaction()
    {
        Func<Task> func = async () => await this.fixture.Context.BeginTransactionAsync();

        // In Memory database, transactions is not working.
        func.Should().Throw<InvalidOperationException>();
    }

    /// <summary>
    /// Test commit transaction.
    /// </summary>
    [Fact]
    public void Test_Commit_Transaction()
    {
        var transaction = new Mock<IDbContextTransaction>();

        Func<Task> func = async () => await this.fixture.Context.CommitTransactionAsync(transaction.Object);

        // In Memory database, transactions is not working.
        func.Should().Throw<InvalidOperationException>();
    }

    /// <summary>
    /// Test rollback transaction.
    /// </summary>
    [Fact]
    public void Test_Rollback_Transaction()
    {
        var transaction = new Mock<IDbContextTransaction>();

        Action func = () => this.fixture.Context.RollbackTransaction();

        // In Memory database, transactions is not working.
        func.Should().NotThrow<InvalidOperationException>();
    }
}