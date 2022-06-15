using Bogus;
using Bogus.DataSets;

namespace Genzai.CosmosDb.Tests.ModelTests;

/// <summary>
/// Domain events test.
/// </summary>
public class DomainEventsTest
{
    /// <summary>
    /// Test Entity Domain Events check created event.
    /// </summary>
    [Fact]
    public void Test_Entity_Domain_Events_Check_Event()
    {
        var clientFaker = new Faker<ClientWithEvents>()
            .CustomInstantiator(x => new ClientWithEvents(
                x.Random.AlphaNumeric(10),
                x.Name.FirstName(Name.Gender.Male),
                x.Name.LastName(),
                x.Random.Int(min: 0, max: 10),
                x.Name.FirstName(Name.Gender.Male)));

        var client = clientFaker.Generate(1).FirstOrDefault();

        client?.DomainEvents.Should().NotBeNull();
        client?.DomainEvents.Should().HaveCount(1);
    }

    /// <summary>
    /// Test Entity Domain Events add event.
    /// </summary>
    [Fact]
    public void Test_Entity_Domain_Events_Add_Event()
    {
        var clientFaker = new Faker<ClientWithEvents>()
            .CustomInstantiator(x => new ClientWithEvents(
                x.Random.AlphaNumeric(10),
                x.Name.FirstName(Name.Gender.Male),
                x.Name.LastName(),
                x.Random.Int(min: 0, max: 10),
                x.Name.FirstName(Name.Gender.Male)));

        var client = clientFaker.Generate(1).FirstOrDefault();

        var eventFaker = new Faker<ClientEditedDomainEvent>()
            .CustomInstantiator(x => new ClientEditedDomainEvent(
                client!,
                x.Random.Int(min: 0, max: 10),
                x.Name.FirstName(Name.Gender.Male)));
        var eventObject = eventFaker.Generate(1).FirstOrDefault();

        client?.AddDomainEvent(eventObject!);

        client?.DomainEvents.Should().NotBeNull();
        client?.DomainEvents.Should().HaveCount(2);
    }

    /// <summary>
    /// Test Entity Domain Events remove event.
    /// </summary>
    [Fact]
    public void Test_Entity_Domain_Events_Remove_Event()
    {
        var clientFaker = new Faker<ClientWithEvents>()
            .CustomInstantiator(x => new ClientWithEvents(
                x.Random.AlphaNumeric(10),
                x.Name.FirstName(Name.Gender.Male),
                x.Name.LastName(),
                x.Random.Int(min: 0, max: 10),
                x.Name.FirstName(Name.Gender.Male)));

        var client = clientFaker.Generate(1).FirstOrDefault();

        var eventFaker = new Faker<ClientEditedDomainEvent>()
            .CustomInstantiator(x => new ClientEditedDomainEvent(
                client!,
                x.Random.Int(min: 0, max: 10),
                x.Name.FirstName(Name.Gender.Male)));
        var eventObject = eventFaker.Generate(1).FirstOrDefault();

        client?.AddDomainEvent(eventObject!);
        client?.RemoveDomainEvent(eventObject!);

        client?.DomainEvents.Should().NotBeNull();
        client?.DomainEvents.Should().HaveCount(1);
    }

    /// <summary>
    /// Test Entity Domain Events clear events.
    /// </summary>
    [Fact]
    public void Test_Entity_Domain_Events_Clear_Events()
    {
        var clientFaker = new Faker<ClientWithEvents>()
            .CustomInstantiator(x => new ClientWithEvents(
                x.Random.AlphaNumeric(10),
                x.Name.FirstName(Name.Gender.Male),
                x.Name.LastName(),
                x.Random.Int(min: 0, max: 10),
                x.Name.FirstName(Name.Gender.Male)));

        var client = clientFaker.Generate(1).FirstOrDefault();

        var eventFaker = new Faker<ClientEditedDomainEvent>()
            .CustomInstantiator(x => new ClientEditedDomainEvent(
                client!,
                x.Random.Int(min: 0, max: 10),
                x.Name.FirstName(Name.Gender.Male)));
        var eventObject = eventFaker.Generate(1).FirstOrDefault();

        client?.AddDomainEvent(eventObject!);
        client?.ClearDomainEvents();

        client?.DomainEvents.Should().NotBeNull();
        client?.DomainEvents.Should().HaveCount(0);
    }

    /// <summary>
    /// Test Entity Domain Events add event and dispatch it.
    /// </summary>
    [Fact]
    public void Test_Entity_Domain_Events_Add_Event_And_Dispatch_It()
    {
        var clientFaker = new Faker<ClientWithEvents>()
            .CustomInstantiator(x => new ClientWithEvents(
                x.Random.AlphaNumeric(10),
                x.Name.FirstName(Name.Gender.Male),
                x.Name.LastName(),
                x.Random.Int(min: 0, max: 10),
                x.Name.FirstName(Name.Gender.Male)));
        var mediator = new Mock<IMediator>();

        var client = clientFaker.Generate(1).FirstOrDefault();

        var eventFaker = new Faker<ClientEditedDomainEvent>()
            .CustomInstantiator(x => new ClientEditedDomainEvent(
                client!,
                x.Random.Int(min: 0, max: 10),
                x.Name.FirstName(Name.Gender.Male)));
        var eventObject = eventFaker.Generate(1).FirstOrDefault();

        client?.AddDomainEvent(eventObject!);

        client?.DomainEvents.Should().NotBeNull();
        client?.DomainEvents.Should().HaveCount(2);

#pragma warning disable CS8631
        Func<Task> func = async () => await mediator.Object.DispatchDomainEventsAsync(client);
#pragma warning restore CS8631

        func.Should().NotThrow();
    }

    /// <summary>
    /// Test Entity to string method.
    /// </summary>
    [Fact]
    public void Test_Entity_ToString_Method()
    {
        var clientFaker = new Faker<ClientWithEvents>()
            .CustomInstantiator(x => new ClientWithEvents(
                x.Random.AlphaNumeric(10),
                x.Name.FirstName(Name.Gender.Male),
                x.Name.LastName(),
                x.Random.Int(min: 0, max: 10),
                x.Name.FirstName(Name.Gender.Male)));

        var client = clientFaker.Generate(1).FirstOrDefault();

        var clientString = client?.ToString();

        clientString.Should().NotBeNullOrEmpty();
    }
}