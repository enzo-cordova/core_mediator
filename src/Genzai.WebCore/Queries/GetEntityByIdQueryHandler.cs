using AutoMapper;
using FluentValidation;
using Genzai.Core.Domain.Model;
using Genzai.EfCore.Repository;
using Genzai.WebCore.Exceptions;
using Genzai.WebCore.Extensions;
using Genzai.WebCore.Responses;
using MediatR;

namespace Genzai.WebCore.Queries;

/// <summary>
/// Entity by id query handler
/// </summary>
/// <typeparam name="TEntity">Entity</typeparam>
/// <typeparam name="TRepository">Repository</typeparam>
/// <typeparam name="TEntityByIdQuery">Entity by id query</typeparam>
/// <typeparam name="TEntityResponse">Entity response</typeparam>
public abstract class GetEntityByIdQueryHandler<TEntity, TRepository, TEntityByIdQuery, TEntityResponse> :
    IRequestHandler<TEntityByIdQuery, TEntityResponse>
    where TRepository : IRepository<TEntity, long>
    where TEntity : class, IEntity<long>
    where TEntityResponse : IEntityResponse
    where TEntityByIdQuery : GetEntityByIdQuery<TEntityResponse>
{
    protected readonly TRepository _repository;
    protected readonly IMapper _mapper;
    protected readonly IValidator<TEntityByIdQuery> _validator;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="repository">Repository</param>
    /// <param name="mapper">Mapper</param>
    /// <param name="validator">Validator</param>
    protected GetEntityByIdQueryHandler(TRepository repository,
        IValidator<TEntityByIdQuery> validator, IMapper mapper)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    /// <summary>
    /// Handle Operations
    /// </summary>
    /// <param name="request">Request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Entity response</returns>
    public async virtual Task<TEntityResponse> Handle(TEntityByIdQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateEntityCommand<TEntity, TEntityByIdQuery>(request);

        var ret = await _repository.GetByIdAsync(request.Id, cancellationToken);
        if (ret == null)
        {
            throw new EntityNotFoundException(typeof(TEntity).Name.ToLower());
        }
        var model = _mapper.Map<TEntity>(ret);
        return GetEntityResponse(model);
    }

    protected virtual TEntityResponse GetEntityResponse(TEntity entityModel)
    {
        return _mapper.Map<TEntityResponse>(entityModel);
    }
}
