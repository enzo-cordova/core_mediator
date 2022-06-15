using Genzai.EfCore.Repository;
using Genzai.Security.Context;
using Genzai.Security.Domain;
using Genzai.Security.Domain.Interfaces;

namespace Genzai.Security.Repository;

/// <summary>
/// RoleRepository
/// </summary>
public class UserRepository : Repository<AuthorizationContext, User, long>, IUserRepository
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    public UserRepository(AuthorizationContext context) : base(context)
    {
    }

    /// <summary>
    /// Save operations
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> SaveAsync(CancellationToken cancellationToken)
    {
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }
}