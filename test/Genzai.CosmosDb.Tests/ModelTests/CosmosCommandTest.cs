using Bogus;

namespace Genzai.CosmosDb.Tests.ModelTests;

/// <summary>
/// Cosmos command test.
/// </summary>
public class CosmosCommandTest
{
    /// <summary>
    /// Cosmos command unit test.
    /// </summary>
    [Fact]
    public void Test_Cosmos_Command()
    {
        var command = new Faker<ClientCommand>()
            .CustomInstantiator(x => new ClientCommand(x.Random.AlphaNumeric(10), x.Random.AlphaNumeric(10)));

        var clientCommand = command.Generate(1).FirstOrDefault();

        clientCommand.Should().NotBeNull();
        clientCommand?.Id.Should().NotBeNullOrWhiteSpace();
        clientCommand?.PartitionKey.Should().NotBeNullOrWhiteSpace();
    }

    /// <summary>
    /// Cosmos command validator unit test.
    /// </summary>
    [Fact]
    public void Test_Cosmos_Command_Validator()
    {
        var command = new Faker<ClientCommand>()
            .CustomInstantiator(x => new ClientCommand(x.Random.AlphaNumeric(10), x.Random.AlphaNumeric(10)));
        var validator = new ClientCommandValidator();

        var clientCommand = command.Generate(1).FirstOrDefault();

        var result = validator.Validate(command);

        clientCommand.Should().NotBeNull();
        clientCommand?.Id.Should().NotBeNullOrWhiteSpace();
        clientCommand?.PartitionKey.Should().NotBeNullOrWhiteSpace();

        result.Should().NotBeNull();
        result.IsValid.Should().BeTrue();
    }
}