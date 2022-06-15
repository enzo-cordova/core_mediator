using Genzai.Core.Telemetry;
using Genzai.Security.Domain;
using Genzai.Security.MiddleWares;
using Genzai.Security.Services.Implementations;
using Genzai.Security.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Genzai.Security.Model;
using Xunit;

namespace Genzai.Security.Tests.Services;

public class TokenServiceTest
{
    private readonly Mock<IPermissionsService> _mockPermissionsService;
    private readonly Mock<ILogger<TokenService>> _mockLogger;
    private readonly Mock<IAutoEnrollmentService<User>> _mockAutoEnrollmentService; 
    private readonly Mock<ITelemetryProvider> _mockTelemetryProvider;
    public TokenServiceTest()
    {
        _mockPermissionsService = new Mock<IPermissionsService>();
        _mockLogger = new Mock<ILogger<TokenService>>();
        _mockAutoEnrollmentService = new Mock<IAutoEnrollmentService<User>>(); 
        _mockTelemetryProvider = new Mock<ITelemetryProvider>();
    }

    [Fact]
    public void TokenService_WhenGenerateInstance_CheckArgument()
    {
        Assert.Throws<ArgumentNullException>(() => new TokenService(null!, _mockAutoEnrollmentService.Object, _mockLogger.Object, _mockTelemetryProvider.Object));
  

        Assert.Throws<ArgumentNullException>(() => new TokenService(_mockPermissionsService.Object, null!, _mockLogger.Object, _mockTelemetryProvider.Object));

        Assert.Throws<ArgumentNullException>(() => new TokenService(_mockPermissionsService.Object,  _mockAutoEnrollmentService.Object, null!, _mockTelemetryProvider.Object));


        Assert.Throws<ArgumentNullException>(() => new TokenService(_mockPermissionsService.Object,  _mockAutoEnrollmentService.Object, _mockLogger.Object, null!));
    }

    [Fact]
    public async Task TokenService_WhenGenerateNewToken_ReturnToken()
    {
        _mockPermissionsService.Setup(s => s.GetPermissionCode(It.IsAny<string>(), default(CancellationToken)))
            .ReturnsAsync(new AuthorizationUser( 2, new List<Role>(){new Role(){Code = "rot"}},null!));
        

        ClaimsIdentity identity = new ClaimsIdentity(
        new Claim[]
        {
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", Guid.NewGuid().ToString()),
            new Claim("http://schemas.microsoft.com/identity/claims/tenantid", "test"),
            new Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", Guid.NewGuid().ToString()),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "test"),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "test"),
            new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn", "test"),
        },
        "test");
        var claimsPrincipal = new ClaimsPrincipal(identity);

        var mockPrincipal = new Mock<IPrincipal>();
        mockPrincipal.Setup(x => x.Identity).Returns(identity);
        mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);
        var tokenService = new TokenService(_mockPermissionsService.Object, _mockAutoEnrollmentService.Object, _mockLogger.Object, _mockTelemetryProvider.Object);
        var resp = await tokenService.NewToken(claimsPrincipal);
        Assert.NotNull(resp);
    }

    [Fact]
    [Trait("TestCategory", "UnitTest")]
    public async Task TokenService_WhenGenerateNewToken_ReturnTokenAndAutoEnrollment()
    {
        _mockPermissionsService.Setup(s => s.GetPermissionCode(It.IsAny<string>(), default(CancellationToken)))
            .ReturnsAsync(new AuthorizationUser(1, new List<Role>() { new Role() { Code = "operator" } }, null!));
        _mockAutoEnrollmentService.Setup(a => a.AddUser(It.IsAny<User>(), default(CancellationToken)));
        ClaimsIdentity identity = new ClaimsIdentity(
            new Claim[]
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", Guid.NewGuid().ToString()),
                new Claim("http://schemas.microsoft.com/identity/claims/tenantid", "test"),
                new Claim("http://schemas.microsoft.com/identity/claims/objectidentifier", Guid.NewGuid().ToString()),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "test"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "test"),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn", "test"),
            },
            "test");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        var mockPrincipal = new Mock<IPrincipal>();
        mockPrincipal.Setup(x => x.Identity).Returns(identity);
        mockPrincipal.Setup(x => x.IsInRole(It.IsAny<string>())).Returns(true);
        var tokenService = new TokenService(_mockPermissionsService.Object,_mockAutoEnrollmentService.Object, _mockLogger.Object, _mockTelemetryProvider.Object);
        var resp = await tokenService.NewToken(claimsPrincipal);
        Assert.NotNull(resp);
    }
}