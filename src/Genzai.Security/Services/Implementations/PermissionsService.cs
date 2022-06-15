using Genzai.Security.Domain;
using Genzai.Security.Domain.Interfaces;
using Genzai.Security.Model;
using Genzai.Security.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Genzai.Security.Services.Implementations;

/// <summary>
/// PermissionsService Implementation
/// </summary>
public class PermissionsService : IPermissionsService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<PermissionsService> _logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="permissionRepository"></param>
    /// <param name="logger"></param>
    /// <param name="userRepository"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public PermissionsService(IPermissionRepository permissionRepository, ILogger<PermissionsService> logger, IUserRepository userRepository)
    {
        _permissionRepository = permissionRepository ?? throw new ArgumentNullException(nameof(permissionRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// Gets the permissions a user has
    /// </summary>
    /// <param name="userCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<AuthorizationUser> GetPermissionCode(string userCode, CancellationToken cancellationToken)
    {
        _logger.LogDebug($"GetPermissionCode - userCode:{ userCode }");
        var permissions = await _permissionRepository.GetAllPermissionsByUserAsync(userCode, cancellationToken); 
     var centers =
            await _userRepository.GetFilteredAsync(s => s.Code == userCode,s=>s.Include(s=>s.Centers),cancellationToken: cancellationToken);

         

        var authorizationUser = new AuthorizationUser(
              permissions.Sum(x => x.Value), permissions.FirstOrDefault().Roles.ToArray(), centers.FirstOrDefault()?.Centers);
        return authorizationUser;
    }
}