namespace Genzai.Security.Services.Interfaces;

/// <summary>
/// IAutoEnrollmentService
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IAutoEnrollmentService<in T>
{
    /// <summary>
    /// AddUser
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> AddUser(T entity, CancellationToken cancellationToken);
}