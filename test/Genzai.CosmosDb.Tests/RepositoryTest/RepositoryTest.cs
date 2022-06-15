namespace Genzai.CosmosDb.Tests.RepositoryTest
{
    /// <summary>
    /// Repository test class.
    /// </summary>
    public class RepositoryTest
    {
        /// <summary>
        /// Test query items
        /// </summary>
        [Fact]
        public void Test_Query_Items()
        {
            var mediator = new Mock<IMediator>();
            var contextMock = new Mock<ICosmosDbContext>();

            var sut = new ClientRepository(mediator.Object, context: contextMock.Object, "test");

            Action action = () => sut.GetItemsAsync().GetAwaiter();

            action.Should().NotThrow();
        }

        [Fact]
        public void Test_Query_Items_by_Id()
        {
            var mediator = new Mock<IMediator>();

            var mongoclientmock = new Mock<MongoClient>();
            var databaseMock = new Mock<IMongoDatabase>();
            var mongoCollectionmock = new Mock<IMongoCollection<Client>>();

            mongoclientmock.Setup(c => c.GetDatabase(It.IsAny<string>(), null)).Returns(databaseMock.Object);

            var sut = new ClientRepository(mediator.Object, mongoCollectionmock.Object);

            Func<Task> action = async () => await sut.GetItemAsync(x => x.ClientId == "1");

            action.Should().NotThrowAsync();
        }

        [Fact]
        public void Test_Add_Item()
        {
            var mediator = new Mock<IMediator>();
            var mongoCollectionmock = new Mock<IMongoCollection<Client>>();
            var sut = new ClientRepository(mediator.Object, mongoCollectionmock.Object);
            var Client = new Client("9", "New Name", "New Last Name");

            Func<Task> action = async () => await sut.AddItemAsync(Client, null, CancellationToken.None);

            action.Should().NotThrowAsync();
        }

        /// <summary>
        /// Test add item.
        /// </summary>
        [Fact]
        public async void Test_Add_Item_Dispatch_Events()
        {
            var mediator = new Mock<IMediator>();
            var mongoCollectionmock = new Mock<IMongoCollection<Client>>();
            var sut = new ClientRepository(mediator.Object, mongoCollectionmock.Object);
            var Client = new Client("9", "New Name", "New Last Name");
            var notification = new Mock<INotification>();
            for (int i = 0; i < 2; i++)
            {
                Client.AddDomainEvent(notification.Object);
            }

            await sut.AddItemAsync(Client, null, CancellationToken.None);

            var events = Client.DomainEvents;

            events.Count().Should().Be(0);
        }

        /// <summary>
        /// Test replace item.
        /// </summary>
        [Fact]
        public void Test_Update_Item()
        {
            var mediator = new Mock<IMediator>();
            var mongoCollectionmock = new Mock<IMongoCollection<Client>>();

            var sut = new ClientRepository(mediator.Object, mongoCollectionmock.Object);
            var mockClient = ObjectMock.GetClients().FirstOrDefault();

            var filter = Builders<Client>.Filter.Eq(mockClient.ClientId, "Client_1");
            var update = Builders<Client>.Update.Set(mockClient.Name, "New Name");
            Func<Task> action = async () => await sut.UpdateOneAsync(mockClient, filter, update);

            action.Should().NotThrowAsync();
        }

        /// <summary>
        /// Test add item.
        /// </summary>
        [Fact]
        public async void Test_Update_Item_Dispatch_Events()
        {
            var mediator = new Mock<IMediator>();
            var mongoCollectionmock = new Mock<IMongoCollection<Client>>();
            var sut = new ClientRepository(mediator.Object, mongoCollectionmock.Object);
            var notification = new Mock<INotification>();
            var mockClient = ObjectMock.GetClients().FirstOrDefault();
            for (int i = 0; i < 2; i++)
            {
                mockClient.AddDomainEvent(notification.Object);
            }

            var filter = Builders<Client>.Filter.Eq(mockClient.ClientId, "Client_1");
            var update = Builders<Client>.Update.Set(mockClient.Name, "New Name");
            await sut.UpdateOneAsync(mockClient, filter, update);

            var events = mockClient.DomainEvents;

            events.Count().Should().Be(0);
        }

        /// <summary>
        /// Test upsert item.
        /// </summary>
        [Fact]
        public void Test_Upsert_Item()
        {
            var mediator = new Mock<IMediator>();
            var mongoCollectionmock = new Mock<IMongoCollection<Client>>();
            var sut = new ClientRepository(mediator.Object, mongoCollectionmock.Object);
            var mockClient = ObjectMock.GetClients().FirstOrDefault();

            mockClient.Name = "New Name";

            Func<Task> action = async () => await sut.UpsertItemAsync(mockClient, mockClient.Name);

            action.Should().NotThrowAsync();
        }

        /// <summary>
        /// Test upsert item.
        /// </summary>
        [Fact]
        public async Task Test_Upsert_Item_Dispatch_EventsAsync()
        {
            var mediator = new Mock<IMediator>();
            var mongoCollectionmock = new Mock<IMongoCollection<Client>>();
            var sut = new ClientRepository(mediator.Object, mongoCollectionmock.Object);
            var mockClient = ObjectMock.GetClients().FirstOrDefault();
            var notification = new Mock<INotification>();
            for (int i = 0; i < 2; i++)
            {
                mockClient.AddDomainEvent(notification.Object);
            }
            mockClient.Name = "New Name";

            await sut.UpsertItemAsync(mockClient, mockClient.Name);

            var events = mockClient.DomainEvents;

            events.Count().Should().Be(0);
        }

        /// <summary>
        /// Test delete item.
        /// </summary>
        [Fact]
        public void Test_Delete_Item()
        {
            var mediator = new Mock<IMediator>();
            var mongoCollectionmock = new Mock<IMongoCollection<Client>>();
            var sut = new ClientRepository(mediator.Object, mongoCollectionmock.Object);
            var mockClient = ObjectMock.GetClients().FirstOrDefault();

            Func<Task> action = async () => await sut.DeleteItemAsync(c => c.ClientId == mockClient.ClientId, CancellationToken.None);

            action.Should().NotThrowAsync();
        }
    }
}