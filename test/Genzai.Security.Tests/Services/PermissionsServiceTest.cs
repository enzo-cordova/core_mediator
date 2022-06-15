using Genzai.Security.Domain;
using Genzai.Security.Domain.Interfaces;
using Genzai.Security.Enums;
using Genzai.Security.Services.Implementations;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Genzai.Security.Tests.Mocks;
using Xunit;

namespace Genzai.Security.Tests.Services;

public class PermissionsServiceTest
{
    private readonly Mock<IPermissionRepository> repository;
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly Mock<ILogger<PermissionsService>> _mockPermissionsService;

    public PermissionsServiceTest()
    {
        repository = new Mock<IPermissionRepository>();
        _mockPermissionsService = new Mock<ILogger<PermissionsService>>();
        _mockUserRepository = new Mock<IUserRepository>();
    }

    [Fact]
    public void AutoEnrollmentService_WhenNullRepositoryPassed_ShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => new PermissionsService(null!, _mockPermissionsService.Object, _mockUserRepository.Object));
        Assert.Throws<ArgumentNullException>(() => new PermissionsService(repository.Object, null!, _mockUserRepository.Object));

        Assert.Throws<ArgumentNullException>(() => new PermissionsService(repository.Object, _mockPermissionsService.Object, null!));
    }

    [Fact]
    public async Task WhenCountingPermissions_ShouldMatchValue()
    {
        const string userCode = "userCode";

        Permission[] permissions =
        {
            new Permission(PermissionTypes.ViewRoles.ToString()),
            new Permission(PermissionTypes.ViewUsers.ToString()),
            new Permission(PermissionTypes.ViewCenters.ToString())
        };
        long expected = (1 << 10) + (1 << 3) + 1;
        _mockUserRepository.Setup(m => m.GetFilteredAsync(It.IsAny<Expression<Func<User, bool>>>(), null, null, false, default(CancellationToken))).ReturnsAsync(MockEntity.GetUsers());

        var p = repository.Setup(x => x.GetAllPermissionsByUserAsync(userCode, CancellationToken.None)).Returns(Task.FromResult((IEnumerable<Permission>)permissions));

        var sut = new PermissionsService(repository.Object, _mockPermissionsService.Object, _mockUserRepository.Object);

        var value = await sut.GetPermissionCode(userCode, default(CancellationToken));

        Assert.Equal(expected, value.Permission);
    }
}