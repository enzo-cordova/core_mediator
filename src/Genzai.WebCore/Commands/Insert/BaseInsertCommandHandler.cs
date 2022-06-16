using AutoMapper;
using FluentValidation;
using Genzai.Core.Domain.Model;
using Genzai.EfCore.Repository;
using Genzai.WebCore.Errors;
using Genzai.WebCore.Exceptions;
using Genzai.WebCore.Extensions;
using Genzai.WebCore.Locales;
using Genzai.WebCore.Requests;
using Genzai.WebCore.Responses;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Genzai.WebCore.Commands.Insert;

/// <summary>
/// Base insert command handler
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
/// <typeparam name="TRepository">Reository</typeparam>
/// <typeparam name="TInsertCommand">Insert command</typeparam>
/// <typeparam name="TEntityInsertRequest">Insert request</typeparam>
/// <typeparam name="TEntityResponse">Entity response</typeparam>
public abstract class BaseInsertCommandHandler<TEntity, TRepository, TInsertCommand, TEntityInsertRequest, TEntityResponse> :
    IRequestHandler<TInsertCommand, TEntityResponse>
    where TRepository : IAuditableRepository<TEntity, long>
    where TEntity : class, IEntity<long>
    where TInsertCommand : BaseInsertCommand<TEntityInsertRequest, TEntityResponse>
    where TEntityInsertRequest : IEntityInsertRequest
    where TEntityResponse : IEntityResponse

{
    protected readonly TRepository _repository;
    protected readonly ILogger<TInsertCommand> _logger;
    protected readonly IMapper _mapper;
    protected readonly IValidator<TInsertCommand> _validator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">Repository</param>
    /// <param name="logger">Logger</param>
    /// <param name="validator">Validator</param>
    /// <param name="mapper">Mapper</param>
    protected BaseInsertCommandHandler(TRepository repository,
        ILogger<TInsertCommand> logger,
        IValidator<TInsertCommand> validator, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Handle operation
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Result</returns>
    public async virtual Task<TEntityResponse> Handle(TInsertCommand request, CancellationToken cancellationToken)
    {
        //Validate command
        await _validator.ValidateEntityCommand<TEntity, TInsertCommand>(request);

        //Entity from request
        TEntity entity = GetEntityFromRequest(request);
        //Validation before save
        IList<ApplicationError> errors = PreSaveValidation(entity);
        if (errors != null && errors.Any())
        {
            throw new CoreValidationException(errors);
        }
        //FIXME
        IAuditable auditableEntity = entity as IAuditable;
        if (auditableEntity != null)
        {
            auditableEntity.CreatedInformation("");
        }
        //Save
        await _repository.AddAsync(entity, cancellationToken);
        var ret = await _repository.SaveAuditableAsync(cancellationToken);
        //Response
        var response = GetResponseFromEntity(entity);
        return ret
            ? response
            : throw new ArgumentException(WebCoreLocalStrings.DataBaseErrorMessage);
    }

    /// <summary>
    /// Entity from request
    /// </summary>
    /// <param name="request">Request</param>
    /// <returns>Entity</returns>
    protected virtual TEntity GetEntityFromRequest(TInsertCommand request)
    {
        TEntity result = _repository.GetEntityDbSet().CreateProxy();
        _mapper.Map(request.Request, result);

        return result;
    }

    /// <summary>
    /// Response from entity
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Response</returns>
    protected virtual TEntityResponse GetResponseFromEntity(TEntity entity)
    {
        return _mapper.Map<TEntityResponse>(entity);
    }
    /// <summary>
    /// Pre save validation
    /// </summary>
    /// <param name="entity">Entity</param>
    /// <returns>Errors</returns>
    protected abstract IList<ApplicationError> PreSaveValidation(TEntity entity);

}
