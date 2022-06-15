using Genzai.Security.Domain;
using Genzai.Security.Domain.Interfaces;
using Genzai.Security.Services.Interfaces;

namespace Genzai.Security.Services.Implementations;

/// <summary>
/// AutoEnrollmentService
/// </summary>
public class AutoEnrollmentService : IAutoEnrollmentService<User>
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userRepository"></param>
    public AutoEnrollmentService(IUserRepository userRepository)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    /// <summary>
    /// AddUser
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<bool> AddUser(User entity, CancellationToken cancellationToken)
    {
        await _userRepository.AddAsync(entity, cancellationToken);
        return await _userRepository.SaveAsync(cancellationToken);
    }
}