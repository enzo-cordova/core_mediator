using Genzai.Security.Model;

namespace Genzai.Security.Services.Interfaces;

/// <summary>
/// interface
/// </summary>
public interface IPermissionsService
{
    /// <summary>
    /// Permissions by user
    /// </summary>
    /// <param name="userCode"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<AuthorizationUser> GetPermissionCode(string userCode, CancellationToken cancellationToken);
}