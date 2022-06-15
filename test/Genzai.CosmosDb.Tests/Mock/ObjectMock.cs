using Bogus;
using Bogus.DataSets;

namespace Genzai.CosmosDb.Tests.Mock;

/// <summary>
/// Object mock.
/// </summary>
public static class ObjectMock
{
    /// <summary>
    /// Get clients mock.
    /// </summary>
    /// <returns>List of clients.</returns>
    public static IEnumerable<Client> GetClients()
    {
        var clientId = 1;
        var clientFaker = new Faker<Client>()
            .CustomInstantiator(x => new Client(
                $"Client_{clientId++}", x.Name.FirstName(Name.Gender.Male), x.Name.LastName()));

        return clientFaker.Generate(10);
    }
}