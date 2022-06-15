using Genzai.Core.Domain.Model;
using Genzai.EfCore.Repository;
using Genzai.EfCore.Search;
using Genzai.WebCore.Commands.Delete;
using Genzai.WebCore.Commands.Insert;
using Genzai.WebCore.Commands.Updates;
using Genzai.WebCore.Exceptions;
using Genzai.WebCore.Requests;
using Genzai.WebCore.Responses;
using Genzai.WebCore.Test.Common;
using System;
using Xunit;

namespace Genzai.WebCore.Test.Handlers
{
    public class BaseCommandHandlerTest<TRepository, TEntity, TEntitySearch, TEntitySearchResult> : BaseHandlerTest<TRepository, TEntity, TEntitySearch, TEntitySearchResult>
        where TRepository : IPartialSearchRepository<TEntity, long, TEntitySearch, TEntitySearchResult>
        where TEntity : class, IEntity<long>
        where TEntitySearch : EntitySearch
        where TEntitySearchResult : EntityIdLongSearchResult
    {


        public BaseCommandHandlerTest(SampleTestContext testContext) : base(testContext)
        {

        }

        public async Task InsertEntityTest<TInsertCommandHandler, TInsertCommand, TEntityInsertRequest,
            TEntityResponse>(TInsertCommandHandler commandHandler, TInsertCommand command, TEntity entityToCreateBeforeHandler, bool expectedError,
            TEntityResponse expectedResponse)
            where TInsertCommandHandler : BaseInsertCommandHandler<TEntity, TRepository, TInsertCommand, TEntityInsertRequest, TEntityResponse>
            where TInsertCommand : BaseInsertCommand<TEntityInsertRequest, TEntityResponse>
            where TEntityInsertRequest : IEntityInsertRequest
            where TEntityResponse : IEntityResponse
        {
            //Comprueba que el handler se ejecuta correctamente
            var cancelToken = new CancellationToken();
            if (entityToCreateBeforeHandler != null)
            {
                await SaveEntity(entityToCreateBeforeHandler);
            }
            if (!expectedError)
            {
                var result = await commandHandler.Handle(command, cancelToken);
                result.Should().NotBeNull();
                if (expectedResponse != null)
                {
                    Dictionary<string, object> resultDictionary = ToDictionary<object>(result);
                    Dictionary<string, object> expectedDictionary = ToDictionary<object>(expectedResponse);
                    Assert.True(DictionaryEquals(expectedDictionary, resultDictionary));
                }
            }
            else
            {
                //Error al insertar
                var ex = Assert.ThrowsAsync<UnauthorizedException>(async () => await commandHandler.Handle(command, cancelToken));
                ex.Should().NotBeNull();
            }

        }



        public async Task UpdateEntityTest<TUpdateCommandHandler, TUpdateCommand, TEntityUpdateRequest>(TUpdateCommandHandler commandHandler, TUpdateCommand command, TEntity entityToCreateBeforeHandler, bool expectedError)
            where TUpdateCommandHandler : BaseUpdateCommandHandler<TEntity, TRepository, TUpdateCommand, TEntityUpdateRequest>
            where TUpdateCommand : BaseUpdateCommand<TEntityUpdateRequest>
            where TEntityUpdateRequest : IEntityUpdateRequest
        {
            //Comprueba que el handler se ejecuta correctamente
            var cancelToken = new CancellationToken();
            if (entityToCreateBeforeHandler != null)
            {
                await SaveEntity(entityToCreateBeforeHandler);
                command.Id = entityToCreateBeforeHandler.Id;
            }
            if (!expectedError)
            {
                var result = await commandHandler.Handle(command, cancelToken);
                result.Should().BeTrue();
            }
            else
            {
                //Error al insertar
                var ex = Assert.ThrowsAsync<Exception>(async () => await commandHandler.Handle(command, cancelToken));
                ex.Should().NotBeNull();
            }

        }

        public async Task DeleteEntityTest<TDeleteCommandHandler, TDeleteCommand>(TDeleteCommandHandler commandHandler,
            TDeleteCommand command, TEntity entityToCreateBeforeHandler, bool expectedError)
            where TDeleteCommandHandler : BaseDeleteCommandHandler<TEntity, TRepository, TDeleteCommand>
        where TDeleteCommand : BaseDeleteCommand
        {
            var cancellationToken = new CancellationToken();
            if (entityToCreateBeforeHandler != null)
            {
                await SaveEntity(entityToCreateBeforeHandler);
                command.Id = entityToCreateBeforeHandler.Id;

            }
            if (!expectedError)
            {

                var result = await commandHandler.Handle(command, cancellationToken);
                result.Should().BeTrue();
            }
            else
            {
                var ex = Assert.ThrowsAsync<Exception>(async () => await commandHandler.Handle(command, cancellationToken));
                ex.Should().NotBeNull();
            }
        }


    }
}
