using Genzai.EfCore.Repository;

namespace Genzai.Security.Domain.Interfaces;

public interface IPermissionRepository : IRepository<Permission, long>
{
    /// <summary>
    /// Get all the permissions granted to the given role.
    /// </summary>
    /// <param name="userCode"></param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<IEnumerable<Permission>> GetAllPermissionsByUserAsync(string userCode, CancellationToken cancellationToken);
}