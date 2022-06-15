using Bogus;
using Genzai.Security.Domain;
using static Genzai.Security.Domain.User;

namespace Genzai.Security.Tests.Mocks;

public static class MockModel
{
    /// <summary>
    /// Get Country Model.
    /// </summary>
    /// <returns>Customer model list.</returns>
    public static User CreateUser()
    {
        return new Faker<User>().CustomInstantiator(f => new User(1, true, f.Person.FirstName,
            f.Person.LastName, f.Random.ToString(), f.Person.Email, f.PickRandom<Authenticated>()));
    }
}