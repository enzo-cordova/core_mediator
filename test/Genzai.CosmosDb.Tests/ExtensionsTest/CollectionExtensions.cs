//namespace Genzai.CosmosDb.Tests.ExtensionsTest;

///// <summary>
///// Collection Extensions.
///// </summary>
//public class CollectionExtensions
//{
//    /// <summary>
//    /// Test To Async enumerable.
//    /// </summary>
//    [Fact]
//    public void Test_To_AsyncEnumerable()
//    {
//        var mockClients = ObjectMock.GetClients();
//        var mockFeedIterator = new Mock<FeedIterator<Client>>();
//        var mockFeedResponse = new Mock<FeedResponse<Client>>();

//        mockFeedResponse.Setup(x => x.GetEnumerator()).Returns(mockClients.GetEnumerator());
//        mockFeedIterator.SetupGet(x => x.HasMoreResults).Returns(true);
//        mockFeedIterator.Setup(x => x.ReadNextAsync(It.IsAny<CancellationToken>()))
//            .Returns(Task.FromResult(mockFeedResponse.Object));

//        var action = mockFeedIterator.Object.ToAsyncEnumerable();

//        action.Should().NotBeNull();
//    }

//    /// <summary>
//    /// Test to async list.
//    /// </summary>
//    [Fact]
//    public void Test_To_Async_List()
//    {
//        CancellationTokenSource cts = new CancellationTokenSource(500);

//        var mockClients = ObjectMock.GetClients();
//        var mockFeedIterator = new Mock<FeedIterator<Client>>();
//        var mockFeedResponse = new Mock<FeedResponse<Client>>();

//        mockFeedResponse.Setup(x => x.GetEnumerator()).Returns(mockClients.GetEnumerator());
//        mockFeedIterator.SetupGet(x => x.HasMoreResults).Returns(true);
//        mockFeedIterator.Setup(x => x.ReadNextAsync(It.IsAny<CancellationToken>()))
//            .Returns(Task.FromResult(mockFeedResponse.Object));

//        var action = mockFeedIterator.Object.ToAsyncEnumerable(cts.Token).ToListAsync(cts.Token);

//        cts.Cancel();

//        action.Should().NotBeNull();
//    }
//}