using Genzai.Security.Domain;
using Genzai.Security.Domain.Interfaces;
using Genzai.Security.Services.Implementations;
using Genzai.Security.Tests.Mocks;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Genzai.Security.Tests.Services;

public class AutoEnrollmentServiceTest
{
    [Fact]
    public void AutoEnrollmentService_WhenNullRepositoryPassed_ShouldThrowException()
    {
        Assert.Throws<ArgumentNullException>(() => new AutoEnrollmentService(null!));
    }

    [Fact]
    public async Task AutoEnrollmentService_WhenNullRepositoryPassed_ShouldReturnOk()
    {
        var mockUserRepository = new Mock<IUserRepository>();
        var model = MockModel.CreateUser();
        mockUserRepository.Setup(u => u.AddAsync(It.IsAny<User>(), default(CancellationToken)));

        mockUserRepository.Setup(u => u.SaveAsync(default(CancellationToken))).ReturnsAsync(true);

        var autoEnrollmentService = new AutoEnrollmentService(mockUserRepository.Object);
        var actual = await autoEnrollmentService.AddUser(It.IsAny<User>(), default(CancellationToken));
        Assert.True(actual);
    }

    [Fact]
    public async Task AutoEnrollmentService_WhenNullRepositoryPassed_ShouldReturnko()
    {
        var mockUserRepository = new Mock<IUserRepository>();
        var model = MockModel.CreateUser();
        mockUserRepository.Setup(u => u.AddAsync(It.IsAny<User>(), default(CancellationToken)));

        mockUserRepository.Setup(u => u.SaveAsync(default(CancellationToken))).ReturnsAsync(false);

        var autoEnrollmentService = new AutoEnrollmentService(mockUserRepository.Object);
        var actual = await autoEnrollmentService.AddUser(It.IsAny<User>(), default(CancellationToken));
        Assert.False(actual);
    }
}