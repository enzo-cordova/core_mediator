using Genzai.EfCore.Repository;
using Genzai.Security.Context;
using Genzai.Security.Domain;
using Genzai.Security.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Genzai.Security.Repository
{
    /// <summary>
    /// PermissionRepository
    /// </summary>
    public class PermissionRepository : Repository<AuthorizationContext, Permission, long>, IPermissionRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public PermissionRepository(AuthorizationContext context) : base(context)
        {
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Permission>> GetAllPermissionsByUserAsync(string userCode, CancellationToken cancellationToken)
        {
            var user = await context.Users
                .AsNoTracking()
                .Include(d => d.Role)
                .ThenInclude(d => d.Permissions)
                .Where(x => x.Code == userCode)
                .FirstOrDefaultAsync(cancellationToken: cancellationToken).ConfigureAwait(false);

            return user?.Role?.Permissions;
        }
    }
}