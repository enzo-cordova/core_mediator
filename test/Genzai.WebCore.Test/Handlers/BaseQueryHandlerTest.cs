using Genzai.Core.Domain.Model;
using Genzai.EfCore.Repository;
using Genzai.EfCore.Search;
using Genzai.WebCore.Queries;
using Genzai.WebCore.Responses;
using Genzai.WebCore.Test.Common;
using System;
using Xunit;

namespace Genzai.WebCore.Test.Handlers
{
    public class BaseQueryHandlerTest<TRepository, TEntity, TEntitySearch, TEntitySearchResult> : BaseHandlerTest<TRepository, TEntity, TEntitySearch, TEntitySearchResult>
        where TRepository : IPartialSearchRepository<TEntity, long, TEntitySearch, TEntitySearchResult>
        where TEntity : class, IEntity<long>
        where TEntitySearch : EntitySearch
        where TEntitySearchResult : EntityIdLongSearchResult
    {


        public BaseQueryHandlerTest(SampleTestContext testContext) : base(testContext)
        {

        }


        public async Task GetEntityByIdTest<TGetEntityByIdQueryHandler, TEntityByIdQuery, TEntityResponse>(
            TGetEntityByIdQueryHandler commandHandler, TEntityByIdQuery command, TEntity entityToCreateBeforeHandler,
            bool expectedError, TEntityResponse expectedResponse)
           where TGetEntityByIdQueryHandler : GetEntityByIdQueryHandler<TEntity, TRepository, TEntityByIdQuery, TEntityResponse>
            where TEntityResponse : IEntityResponse
            where TEntityByIdQuery : GetEntityByIdQuery<TEntityResponse>
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
                var ex = Assert.ThrowsAsync<Exception>(async () => await commandHandler.Handle(command, cancelToken));
                ex.Should().NotBeNull();
            }

        }


        public async Task GetEntitySearchTest<TGetEntitySearchQueryHandler, TEntitySearchQuery, TEntitySearchRequest, TEntitySearchResponse>(
           TGetEntitySearchQueryHandler commandHandler, TEntitySearchQuery command, TEntity entityToCreateBeforeHandler,
           bool expectedError, int expectedCount, TEntitySearchResponse expectedResponse)
        where TGetEntitySearchQueryHandler : GetEntitySearchQueryHandler<TEntity, TEntitySearch, TEntitySearchResult, TRepository, TEntitySearchRequest, TEntitySearchQuery, TEntitySearchResponse>
        where TEntitySearchRequest : EntitySearch
        where TEntitySearchResponse : IEntitySearchResponse
        where TEntitySearchQuery : GetEntitySearchQuery<TEntitySearchRequest, TEntitySearchResponse>
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
                Assert.True(result.SearchSize == expectedCount);
                if (expectedCount > 0)
                {
                    result.Items.Should().NotBeEmpty();
                    TEntitySearchResponse firstResult = result.Items.First();
                    Dictionary<string, object> resultDictionary = ToDictionary<object>(firstResult);
                    Dictionary<string, object> expectedDictionary = ToDictionary<object>(expectedResponse);
                    Assert.True(DictionaryEquals(expectedDictionary, resultDictionary));
                }
            }
            else
            {
                //Error al buscar
                var ex = Assert.ThrowsAsync<Exception>(async () => await commandHandler.Handle(command, cancelToken));
                ex.Should().NotBeNull();
            }

        }

    }
}
