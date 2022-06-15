using Genzai.Security.Extensions;
using System;
using System.Security.Claims;
using Xunit;

namespace Genzai.Security.Tests.Extensions;

public class ClaimsPrincipalExtensionsTest
{
    [Fact]
    public void ClaimsPrincipalExtensionsTest_Exception()
    {
        Assert.Throws<ArgumentNullException>(() => ClaimsPrincipalExtensions.GetObjectId(null!));
    }

    [Fact]
    public void ClaimsPrincipalExtensionsTest_GetObjectId()
    {
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
        string acction = ClaimsPrincipalExtensions.GetObjectId(claimsPrincipal);
        Assert.NotNull(acction);
    }
}