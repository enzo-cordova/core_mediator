namespace Genzai.CosmosDb.Tests.Mock.Entities;

/// <summary>
/// Client command test.
/// </summary>
public class ClientCommand : CosmosCommand
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientCommand"/> class.
    /// </summary>
    /// <param name="id">Entity Id.</param>
    /// <param name="partitionKey">Partition key.</param>
    public ClientCommand(string id, string partitionKey)
        : base(id, partitionKey)
    {
    }
}

/// <summary>
/// Client Command Validator
/// </summary>
public class ClientCommandValidator : CosmosCommandValidator<ClientCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ClientCommandValidator"/> class.
    /// </summary>
    public ClientCommandValidator()
    { }
}