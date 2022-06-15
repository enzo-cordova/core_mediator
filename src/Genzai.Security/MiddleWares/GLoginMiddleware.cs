using Genzai.Core.Caching;
using Genzai.Core.Telemetry;
using Genzai.Security.Extensions;
using Genzai.Security.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Genzai.Security.MiddleWares;

/// <summary>
/// G-Edge Authorization middleware
/// </summary>
public class GLoginMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="next"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public GLoginMiddleware(RequestDelegate next)
    {
        _next = next ?? throw new ArgumentNullException(nameof(next));        
    }

    /// <summary>
    /// Invocation
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="tokenService"></param>
    /// <param name="logger"></param>
    /// <param name="responseCacheService"></param>
    /// <param name="telemetryProvider"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentException"></exception>
    public async Task Invoke(HttpContext httpContext, ITokenService tokenService, ILogger<GLoginMiddleware> logger, IResponseCacheService responseCacheService, ITelemetryProvider telemetryProvider)
    {
        try
        { 
            logger.LogDebug($"GLoginMiddleware start middleware");
            if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));
            logger.LogDebug($"GLoginMiddleware health");
            if (httpContext.Request.Path.Value!.Equals("/health"))
                return;
            if (httpContext.User == null) throw new ArgumentNullException(nameof(httpContext.User));
            if (httpContext.User.Identity == null) throw new ArgumentNullException(nameof(httpContext.User.Identity));
            if (!httpContext.User.Identity.IsAuthenticated) throw new ArgumentException($"It's not Authenticated"); 
            telemetryProvider.AddEventProperty("Identity", httpContext.User.Identity.ToString());
            telemetryProvider.TrackEvent("GLoginMiddleware");
            var isValidToken = false;
            var userCode = httpContext.User.GetObjectId(); 
            var cacheResponse = await responseCacheService.GetCachedResponseAsync<GToken>(userCode); 
            if (cacheResponse!=null)
            {
                if (await tokenService.VerifyToken(cacheResponse, userCode))
                    isValidToken = true;
            }
            if (cacheResponse == null || !isValidToken)
            {
                var token = await tokenService.NewToken(httpContext.User); 
                await responseCacheService.CachedResponseAsync(userCode, token, TimeSpan.FromMinutes(60)); 
            }
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await HttpResponseWritingExtensions.WriteAsync(httpContext.Response, "{\"message\": \"Unauthorized\"}");
        }
    }
}