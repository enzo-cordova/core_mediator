using Genzai.Security.Context;
using Genzai.Security.Repository;
using Genzai.Security.Tests.Mocks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;
using System.Security.Claims;

namespace Genzai.Security.Tests.Fixtures;

public class DatabaseFixture : IDisposable
{
    private bool disposedValue = false;

    /// <summary>
    /// Database context.
    /// </summary>
    public AuthorizationContext Context { get; }

    /// <summary>
    /// User repository.
    /// </summary>
    public UserRepository UserRepository { get; private set; }

    /// <summary>
    /// PermissionRepository
    /// </summary>
    public PermissionRepository PermissionRepository { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DatabaseFixture"/> class.
    /// </summary>
    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder<AuthorizationContext>().UseInMemoryDatabase(databaseName: "gwatchtest")
            .Options;

        var mediator = new Mock<IMediator>();
        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new("emails", "mock@email.fake"),
            new(ClaimTypes.Name, "anynameazuread@prosegur.com")
        }, "mock"));

        Context = new AuthorizationContext(options, mediator.Object, claimsPrincipal);

        PermissionRepository = new PermissionRepository(Context);
        UserRepository = new UserRepository(Context);

        var roles = MockEntity.GetRoles();
        var permissions = MockEntity.GetPermissions();
        Context.Permissions.Add(permissions.First());
        Context.SaveChangesAsync();
        var users = MockEntity.GetUsers();
        var user = users.First();
        user.UpdateRole(roles.First());
        Context.Users.Add(users.First());
        Context.SaveChangesAsync();
    }

    /// <summary>
    /// Dispose method.
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
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
                PermissionRepository = null!;
                UserRepository = null!;
                Context.Dispose();
            }

            disposedValue = true;
        }
    }
}