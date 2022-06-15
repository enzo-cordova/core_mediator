//namespace Genzai.CosmosDb.Extensions;

///// <summary>
///// Collection extensions helper.
///// </summary>
//public static class CollectionExtensions
//{
//    /// <summary>
//    /// Transform Feed Iterator to AsyncEnumerable.
//    /// </summary>
//    /// <typeparam name="T">Generic Type.</typeparam>
//    /// <param name="iterator">Cosmos iterator.</param>
//    /// <param name="cancellationToken">Cancellation token.</param>
//    /// <returns>AsyncEnumerable collection.</returns>
//    public static async IAsyncEnumerable<T> ToAsyncEnumerable<T>(
//        this FeedIterator<T> iterator,
//        [EnumeratorCancellation] CancellationToken cancellationToken = default)
//    {
//        while (iterator.HasMoreResults && !cancellationToken.IsCancellationRequested)
//        {
//            foreach (var item in await iterator.ReadNextAsync(cancellationToken))
//            {
//                yield return item;
//            }
//        }
//    }

//    /// <summary>
//    /// AsyncEnumerable to List.
//    /// </summary>
//    /// <typeparam name="T">Generic Type.</typeparam>
//    /// <param name="source">Async Enumerable.</param>
//    /// <param name="cancellationToken">Cancellation token.</param>
//    /// <returns>Object List.</returns>
//    public static async Task<List<T>> ToListAsync<T>(
//        this IAsyncEnumerable<T> source,
//        CancellationToken cancellationToken = default)
//    {
//        var list = new List<T>();

//        await foreach (var item in source.WithCancellation(cancellationToken))
//        {
//            list.Add(item);
//        }

//        return list;
//    }
//}