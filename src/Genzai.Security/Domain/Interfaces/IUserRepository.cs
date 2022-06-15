using Genzai.EfCore.Repository;

namespace Genzai.Security.Domain.Interfaces;

/// <inheritdoc />
public interface IUserRepository : IRepository<User, long>
{
    /// <summary>
    /// Saves the asynchronous.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns></returns>
    Task<bool> SaveAsync(CancellationToken cancellationToken);
}