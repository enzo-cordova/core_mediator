using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Genzai.Security.Tests.Mocks;
internal static class MockControllerContext
{
    public static ControllerContext GetHttpContext()
    {
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new("emails", "mock@email.fake"),
            new(ClaimTypes.Name, "anynameazuread@prosegur.com")
        }, "mock"));

        return new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
    }
}
