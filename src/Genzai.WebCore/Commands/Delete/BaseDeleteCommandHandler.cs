using FluentValidation;
using Genzai.Core.Domain.Model;
using Genzai.EfCore.Repository;
using Genzai.WebCore.Exceptions;
using Genzai.WebCore.Extensions;
using MediatR;

namespace Genzai.WebCore.Commands.Delete;

/// <summary>
/// Base delete comman handler
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
/// <typeparam name="TRepository">Repository</typeparam>
/// <typeparam name="TDeleteCommand">Delete command</typeparam>
public abstract class BaseDeleteCommandHandler<TEntity, TRepository, TDeleteCommand> : IRequestHandler<TDeleteCommand, bool>
    where TRepository : IRepository<TEntity, long>
    where TEntity : class, IEntity<long>
    where TDeleteCommand : BaseDeleteCommand
{
    protected readonly TRepository _repository;
    protected readonly IValidator<TDeleteCommand> _validator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">Repository</param>
    /// <param name="validator">Validator</param>
    protected BaseDeleteCommandHandler(TRepository repository,
        IValidator<TDeleteCommand> validator)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handle operation
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Delete result</returns>
    public virtual async Task<bool> Handle(TDeleteCommand request, CancellationToken cancellationToken)
    {
        // Valida request
        await _validator.ValidateEntityCommand<TEntity, TDeleteCommand>(request);
        // Comprueba que exista la entidad
        var entity = await _repository.GetByIdAsync(request.Id,
            cancellationToken: cancellationToken).ConfigureAwait(false);
        if (entity == null)
            throw new EntityNotFoundException(typeof(TEntity).Name.ToLower());
        //Borra
        _repository.Delete(entity);
        var ret = await _repository.SaveAsync(cancellationToken);
        return ret;
    }
}
