using AutoMapper;
using FluentValidation;
using Genzai.Core.Domain.Model;
using Genzai.EfCore.Repository;
using Genzai.WebCore.Errors;
using Genzai.WebCore.Exceptions;
using Genzai.WebCore.Extensions;
using Genzai.WebCore.Requests;
using MediatR;


namespace Genzai.WebCore.Commands.Updates;

/// <summary>
/// Base update command handler
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
/// <typeparam name="TRepository">Repository</typeparam>
/// <typeparam name="TUpdateCommand">Update command</typeparam>
/// <typeparam name="TEntityUpdateRequest">Update request</typeparam>
public abstract class BaseUpdateCommandHandler<TEntity, TRepository, TUpdateCommand, TEntityUpdateRequest> :
    IRequestHandler<TUpdateCommand, bool>
    where TEntityUpdateRequest : IEntityUpdateRequest

    where TRepository : IRepository<TEntity, long>
    where TEntity : class, IEntity<long>
    where TUpdateCommand : BaseUpdateCommand<TEntityUpdateRequest>
{
    protected readonly TRepository _repository;
    protected readonly IValidator<TUpdateCommand> _validator;
    protected readonly IMapper _mapper;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">Repository</param>
    /// <param name="validator">Validator</param>
    /// <param name="mapper">Mapper</param>
    protected BaseUpdateCommandHandler(TRepository repository,
        IValidator<TUpdateCommand> validator, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    }

    /// <summary>
    /// Handle operation
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Cancelation token</param>
    /// <returns>Update result</returns>
    public async virtual Task<bool> Handle(TUpdateCommand request, CancellationToken cancellationToken)
    {
        //Validate command
        await _validator.ValidateEntityCommand<TEntity, TUpdateCommand>(request);
        //Retrieve entity from database
        var oldFromDatabase = await _repository.GetByIdAsync(request.Id, cancellationToken: cancellationToken);
        //Check entity exists
        if (oldFromDatabase == null)
            throw new EntityNotFoundException(typeof(TEntity).Name.ToLower());
        //Validate old entity and request
        IList<ApplicationError> errors = PreUpdateValidation(oldFromDatabase, request.Request);
        CheckErrors(errors);
        //Entity from request
        TEntity newEntity = GetUpdatedEntityFromRequest(oldFromDatabase, request);
        //Validate entity before save
        errors = PreUpdateValidation(newEntity);
        CheckErrors(errors);
        //Update entity
        _repository.UpdateEntityIntoDbSet(newEntity);
        var ret = await _repository.SaveAsync(cancellationToken);

        return ret;
    }

    /// <summary>
    /// Entity from request
    /// </summary>
    /// <param name="oldEntity">Old entity</param>
    /// <param name="request">Request</param>
    /// <returns>Entity</returns>
    protected virtual TEntity GetUpdatedEntityFromRequest(TEntity oldEntity, TUpdateCommand request)
    {
        return _mapper.Map(request.Request, oldEntity);
    }

    /// <summary>
    /// Pre update validation
    /// </summary>
    /// <param name="oldFromDatabase">Old database entity</param>
    /// <param name="updateRequest">Update request</param>
    /// <returns>Errores</returns>
    protected abstract IList<ApplicationError> PreUpdateValidation(TEntity oldFromDatabase, TEntityUpdateRequest updateRequest);

    /// <summary>
    /// Pre update validationPre update validation
    /// </summary>
    /// <param name="newEntity">Entity</param>
    /// <returns>Errores</returns>
    protected abstract IList<ApplicationError> PreUpdateValidation(TEntity newEntity);

    private static void CheckErrors(IList<ApplicationError> errors)
    {
        if (errors != null && errors.Any())
        {
            throw new CoreValidationException(errors);
        }
    }
}
