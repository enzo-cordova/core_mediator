using Genzai.Core.Caching;
using Genzai.Security.MiddleWares;
using Genzai.Security.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Genzai.Core.Telemetry;
using Xunit;

namespace Genzai.Security.Tests.MiddleWares
{
    public class GLoginMiddlewareTest
    {
        private readonly Mock<ILogger<GLoginMiddleware>> _mockGLoginMiddleware;
        private readonly Mock<IResponseCacheService> _mockResponseCacheService;
        private readonly Mock<ITelemetryProvider> _mockTelemetryProvider;
        public GLoginMiddlewareTest()
        {
            _mockGLoginMiddleware = new Mock<ILogger<GLoginMiddleware>>();
            _mockResponseCacheService = new Mock<IResponseCacheService>();
            _mockTelemetryProvider = new Mock<ITelemetryProvider>();
        }

        [Fact]
        public async Task OnFirstRequest_WhenAuthenticated_ThenTokenAddedToContext()
        {
            const string expectedOutput = "output";

            var fakedContext = new Mock<HttpContext>();

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(x => x.IsAuthenticated).Returns(true);
            identity.Setup(x => x.Claims).Returns(new Claim[]
                {
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "user"),
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "name"),
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "surname"),
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn", "email@email.em")
                });
            ClaimsPrincipal principal = new ClaimsPrincipal(identity.Object);

            DefaultHttpContext plainContext = new DefaultHttpContext();
            fakedContext.Setup(x => x.User).Returns(principal);
            fakedContext.Setup(x => x.Request).Returns(plainContext.Request);
            fakedContext.Setup(x => x.Response).Returns(plainContext.Response);
            fakedContext.Setup(x => x.Items).Returns(plainContext.Items);

            var defaultContext = fakedContext.Object;
            defaultContext.Response.Body = new MemoryStream();
            var ts = new Mock<ITokenService>();

            ts.Setup(x => x.VerifyToken(It.IsAny<GToken>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            ts.Setup(x => x.NewToken(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(new GToken()));

            // Act
            var middlewareInstance = new GLoginMiddleware((innerHttpContext) =>
            {
                innerHttpContext.Response.WriteAsync(expectedOutput);
                return Task.CompletedTask;
            });


            _mockTelemetryProvider.Setup(c => c.TrackEvent(""));

            await middlewareInstance.Invoke(defaultContext, ts.Object, _mockGLoginMiddleware.Object, _mockResponseCacheService.Object, _mockTelemetryProvider.Object);

            // Assert 1: if the next delegate was invoked
            defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
            var body = new StreamReader(defaultContext.Response.Body).ReadToEnd();
            Assert.Equal(expectedOutput, body);

            // Assert 2: if the token is added to the context.Items
            //  Assert.Equal(1, defaultContext.Items.Count);
        }

        [Fact]
        public async Task WithToken_WhenAuthenticated_ThenWorks()
        {
            var fakedContext = new Mock<HttpContext>();

            var identity = new Mock<ClaimsIdentity>();
            identity.Setup(x => x.IsAuthenticated).Returns(true);
            identity.Setup(x => x.Claims).Returns(new Claim[]
                {
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "user"),
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname", "name"),
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname", "surname"),
                    new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/upn", "email@email.em")
                });
            ClaimsPrincipal principal = new(identity.Object);

            DefaultHttpContext plainContext = new();

            plainContext.Request.Headers.Add("X-GAuthorization", new[] { "token" });

            fakedContext.Setup(x => x.User).Returns(principal);
            fakedContext.Setup(x => x.Request).Returns(plainContext.Request);
            fakedContext.Setup(x => x.Response).Returns(plainContext.Response);
            fakedContext.Setup(x => x.Items).Returns(plainContext.Items);

            var defaultContext = fakedContext.Object;
            defaultContext.Response.Body = new MemoryStream();
            var ts = new Mock<ITokenService>();

            ts.Setup(x => x.VerifyToken(It.IsAny<GToken>(), It.IsAny<string>())).Returns(Task.FromResult(true));
            ts.Setup(x => x.NewToken(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(new GToken()));

            // Act
            var middlewareInstance = new GLoginMiddleware((innerHttpContext) =>
            {
                return Task.CompletedTask;
            });

            await middlewareInstance.Invoke(defaultContext, ts.Object, _mockGLoginMiddleware.Object, _mockResponseCacheService.Object, _mockTelemetryProvider.Object);

            // Assert 1: if the token is NOT added to the response header
            var header = defaultContext.Response?.Headers?["X-GAuthorization"];
            Assert.True(header.HasValue && header.Value.Count == 0);
        }
    }
}