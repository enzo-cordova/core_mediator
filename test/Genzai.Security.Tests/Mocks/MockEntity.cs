using Bogus;
using Genzai.Security.Domain;
using System.Collections.Generic;

namespace Genzai.Security.Tests.Mocks;

public class MockEntity
{
    /// <summary>
    /// Get
    /// </summary>
    /// <returns>Get Roles</returns>
    public static IEnumerable<User> GetUsers()
    {
        var roleFake = new Faker<User>()
            .RuleFor(c => c.Name, "NOMBRE-TEST")
            .RuleFor(c => c.FamilyName, f => f.Name.LastName())
            .RuleFor(c => c.RoleId, f => f.Random.Long(2))
            .RuleFor(c => c.Active, true)
            .RuleFor(c => c.Email, f => f.Internet.Email())
            .RuleFor(c => c.Code, f => f.Random.AlphaNumeric(25))
            .RuleFor(c => c.TypeAuthenticated, f => f.PickRandom<User.Authenticated>());
        
        return roleFake.Generate(30);
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <returns>Get Roles</returns>
    public static IEnumerable<Permission> GetPermissions()
    {
        var roleFake = new Faker<Permission>()
            .RuleFor(c => c.Id, f => f.Random.Long(1))
            .RuleFor(c => c.Name, f => f.Random.AlphaNumeric(25))
            .RuleFor(c => c.Value, f => f.Random.Long(1 << 16));

        return roleFake.Generate(30);
    }

    /// <summary>
    /// Get
    /// </summary>
    /// <returns>Get Roles</returns>
    public static IEnumerable<Role> GetRoles()
    {
        var roleFake = new Faker<Role>()
            .RuleFor(c => c.Name, f => f.Random.AlphaNumeric(10))
            .RuleFor(c => c.Description, f => f.Random.AlphaNumeric(100))
            .RuleFor(c => c.Code, f => f.Random.AlphaNumeric(10));

        return roleFake.Generate(30);
    }
}