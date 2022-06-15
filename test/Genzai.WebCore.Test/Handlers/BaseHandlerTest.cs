using Genzai.Core.Domain.Model;
using Genzai.EfCore.Repository;
using Genzai.EfCore.Search;
using Genzai.WebCore.Test.Common;
using Genzai.WebCore.Test.Mock.Infrastructure.Data.Context;
using Xunit;

namespace Genzai.WebCore.Test.Handlers
{
    public class BaseHandlerTest<TRepository, TEntity, TEntitySearch, TEntitySearchResult> : IClassFixture<SampleTestContext>
        where TRepository : IPartialSearchRepository<TEntity, long, TEntitySearch, TEntitySearchResult>
        where TEntity : class, IEntity<long>
        where TEntitySearch : EntitySearch
        where TEntitySearchResult : EntityIdLongSearchResult
    {
        protected readonly SampleContext context;
        protected readonly IMediator mediator;
        protected readonly IMapper mapper;
        protected TRepository repository { get; set; }

        public BaseHandlerTest(SampleTestContext testContext)
        {
            context = testContext.SampleContext;
            mediator = testContext.Mediator;
            mapper = testContext.Mapper;
        }


        protected async Task SaveEntity(TEntity entity)
        {
            var cancellationToken = new CancellationToken();
            await repository.AddAsync(entity, cancellationToken);
            await repository.SaveAsync(cancellationToken);
        }

        public static bool DictionaryEquals(Dictionary<string, object> expectedDictionary, Dictionary<string, object> dictionary)
        {
            foreach (KeyValuePair<string, object> item in expectedDictionary)
            {
                if (item.Value != null && !item.Key.Equals("CopyNull") && !item.Key.Equals("Id") &&
                    dictionary[item.Key] != null &&
                    !dictionary[item.Key]!.ToString()!.Equals(item.Value.ToString()))
                {
                    return false;
                }

            }
            return true;
        }
        public static Dictionary<string, TValue> ToDictionary<TValue>(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            Dictionary<string, TValue> dictionary = JsonConvert.DeserializeObject<Dictionary<string, TValue>>(json)!;
            return dictionary;
        }
    }
}
