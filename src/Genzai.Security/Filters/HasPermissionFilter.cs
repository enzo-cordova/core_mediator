using Genzai.Core.Caching;
using Genzai.Security.Enums;
using Genzai.Security.Extensions;
using Genzai.Security.MiddleWares; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis; 
using Newtonsoft.Json;

namespace Genzai.Security.Filters;

/// <summary>
/// Filter
/// </summary>
[ExcludeFromCodeCoverage]
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class HasPermissionFilter : Attribute, IAsyncAuthorizationFilter
{ 
    private readonly IResponseCacheService _responseCacheService;
    private readonly ILogger<HasPermissionFilter> _logger;

    /// <summary>
    /// Value containing permissions set required
    /// </summary>
    public long Policies { get; private set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="policies"></param> 
    /// <param name="logger"></param>
    /// <param name="responseCacheService"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public HasPermissionFilter(PermissionTypes policies, ILogger<HasPermissionFilter> logger, IResponseCacheService responseCacheService)
    {
        Policies = (long)policies;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _responseCacheService = responseCacheService ?? throw new ArgumentNullException(nameof(responseCacheService));
    }

    /// <summary>
    /// OnAuthorization
    /// </summary>
    /// <param name="context">context</param>
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userCode = context.HttpContext.User.GetObjectId();
        _logger.LogDebug("OnAuthorizationAsync userCode: {userCode}", userCode);
        var cachedResponse = await _responseCacheService.GetCachedResponseAsync(userCode);
        long userPermissions = 0;
        if (!string.IsNullOrEmpty(cachedResponse))
        {
            _logger.LogDebug("OnAuthorizationAsync DecryptTokenCache: {cachedResponse}", cachedResponse);
   
            var token = JsonConvert.DeserializeObject<GToken>(cachedResponse);
            if (token != null)
            {
                userPermissions = token.PermissionValue;
            }
        }
        else
        {
            _logger.LogDebug("OnAuthorizationAsync ForbidResult");
            context.Result = new ForbidResult();
            return;
        }
        _logger.LogDebug("OnAuthorizationAsync userPermissions: {userPermissions}", userPermissions);
        _logger.LogDebug("OnAuthorizationAsync Policies: {Policies}", Policies);
        if ((userPermissions & Policies) == Policies)
        {
            _logger.LogInformation("OnAuthorizationAsync userPermissions & Policies: {Result}", userPermissions & Policies);
            return;
        }
        context.Result = new ForbidResult();
    }
}