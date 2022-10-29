https://learn.microsoft.com/en-us/answers/questions/723164/granting-read-privileges-to-azure-artifact-feed.html

# ![alt text](GENZAI.png "Genzai Logo") 

Common libraries for Genzai projects.

## Build and Test

- Before commit your code, just compile with **ctrl+shift+b** and execute unit tests with test explorer.
- Every class and method must have it's unit test.

## Git commits

- Small commits.
- Every day get latest changes.

## Projects descriptions

### Genzai.Core

Library that contains helpers and extensions classes and also domain base classes.

#### How to implement an **KeyLessEntity**

```C#
public class CarBase : EntityBase<Car>
{
    public string Brand { get; set; }

    public string Model { get; set; }

    public override bool Equals(object obj)
    {
        return this.Equals(obj as Car);
    }

    public override int GetHashCode()
    {
        return System.HashCode.Combine(base.GetHashCode());
    }
}

  modelBuilder.Entity<CarBaseTest>(entity =>
        {
            entity.HasKey(x => new { x.Brand, x.Model });
            
            entity.Property(a => a.Brand)
                .IsRequired();
            entity.Property(a => a.Model)
                .IsRequired();
            
           
        });
		
if we are using a view then

 modelBuilder.Entity<CarBaseTest>(entity =>
        {
            entity.HasNoKey();
            
            (...)
           
        });
```

#### How to implement an **AuditableEntity**

```C#
public class Car : AuditableEntity<Car, int>, IAuditable
{
    public string Brand { get; set; }

    public string Model { get; set; }

    public override bool Equals(object obj)
    {
        return this.Equals(obj as Car);
    }

    public override int GetHashCode()
    {
        return System.HashCode.Combine(base.GetHashCode());
    }
}
```


AuditableEntity, automaticly implement method to inform about CreatedInformation, UpdateInformation


#### How to implement an **Entity**

```C#
public class Car : Entity<Car, int>
{
    public string Brand { get; set; }

    public string Model { get; set; }

    public override bool Equals(object obj)
    {
        return this.Equals(obj as Car);
    }

    public override int GetHashCode()
    {
        return System.HashCode.Combine(base.GetHashCode());
    }
}
```

#### How to implement an **EntityWithEvents**

```C#
public class Order : EntityWithEvents<Order, int>
{
    public Order()
    {
        this.OrderItems = new List<OrderItem>();
    }

    public Order(int buyerId, string description, Address address, int userId, string userName)
        : this()
    {
        this.BuyerId = buyerId;
        this.Description = description;
        this.Address = address;

        this.OrderItems = new HashSet<OrderItem>();

        this.AddOrderStartedDomainEvent(userId, userName);
    }

    public int BuyerId { get; set; }

    public string Description { get; set; }

    public Address Address { get; }

    public Buyer Buyer { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; }

    public void AddOrderItem(int productId, string productName, decimal unitPrice, int units = 1)
    {
        var existingOrder = this.OrderItems.SingleOrDefault(x => x.ProductId == productId);

        if (existingOrder == null)
        {
            this.OrderItems.Add(new OrderItem(productId, productName, unitPrice, units));
        }
        else
        {
            existingOrder.AddUnits(units);
        }
    }

    private void AddOrderStartedDomainEvent(int userId, string userName)
    {
        var domainEvent = new OrderStartedDomainEvent(this, userId, userName);

        this.AddDomainEvent(domainEvent);
    }
}
```

#### How to add Genzai Security JWT Bearer configuration

In startup class of our web or api project:

```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    //Configuration service.
    services.AddTcmAuthenticationBearer(this.configuration);
}
```

### Genzai.CosmosDb

Library that contains repository pattern for Azure Cosmos Db.

#### How to add in a new project

In startup class of our web or api project:

```C#
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    services.AddCosmosDb(new ClientConfiguration
    {
        Endpoint = "",
        ApiKey = "",
        DatabaseName = "",
        Options = new CosmosClientOptions { ApplicationName = "TestMyCosmos" }
    });
}
```

#### How to create an Entity with domain events

Just inherit **CosmosEntityDomain** class in your Entity class:

```C#
public class Entity : CosmosEntityDomain
{
    private void TestMethod()
    {
        this.AddDomainEvent(new DomainEvent());
    }
}
```

#### How to Create Madiatr Command with CosmosCommand

Just inherit **CosmosCommand** clas in your Command class:

```C#
public class Command : CosmosCommand, IRequest<ResponseValue>
{
    public Command(string id, string partitionKey)
        : base(id, partitionKey)
    {
    }
}
```

#### How to Validate a CommandHandler

```C#
public class CommandHandler : IRequestHandler<CommandToValidate, Value>
{
    private readonly IRepository repository;
    private readonly IValidator<Command> validator;

    public CommandHandler(
        IRepository repository,
        IValidator<CommandToValidate> validator)
    {
        this.repository = repository;
        this.validator = validator;
    }

    public async Task<Value> Handle(
        CommandToValidate request, CancellationToken cancellationToken)
    {
        // Validation is here.
        await this.validator.ValidateCommand(request);
    }
}
```

### Genzai.EfCore

Library that contains repository pattern for Entity Framework Core. This package is made thinking about relational databases:

- MySql
- Azure Sql Database

#### How to Scaffold database in project

First of all we have to install the following packages in our project:

- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.SqlServer.Design
- Microsoft.EntityFrameworkCore.Tools
- Pomelo.EntityFrameworkCore.MySql

Next step, open **CMD** console and go to the project folder and execute the following script:

```Shell

dotnet ef dbcontext scaffold "Server={Sql Server Instance};Database={Database name};Trusted_Connection=True;User ID={user};Password={Password};MultipleActiveResultSets=true" Pomelo.EntityFrameworkCore.MySql -o {FolderName} -c "{Context name}"

```



## Version Control

### Next

To Be Defined

### Version 1.0.9

Changes
- AuditableEntity Creation.
- New Context Method for automatic track changes when save operation. Necesary to inform about UserPrincipalClaim
- Common services for ServiceBus
- Custom Searchable Attribute for filtering, paging, ordering, and sorting.
- Api MongoDb for CosmosDb
- Securization.
- Vault Securization



### Version 1.0.8

Changes:

- Updated dependencies
- Included **AsyncQueryable** as an extension in Genzai.EfCore.

### Version 1.0.7

Changes:

- Unit testing dependencies updated.
- Cosmos db updated.
- Ef core map class modified. Now allows strings as keys.

### Version 1.0.6

Changes:

- Usings reordered.
- Upsert method in Cosmos Repository.
- Security extension for JWT token bearer authentication.
- Exception context extensions.

### Version 1.0.5

Changes:

- Changed EF base entities for accepting strings ids.
- Changed EF repository for accepting strings ids.

### Version 1.0.4

Changes:

- Updated EFCore dependencies.
- Added Sql connection configuration entity.

### Version 1.0.3

Changes:

- Simplified state machine in all mediator extensions.
- Added overload GetItemsAsync in CosmosDb Repo.
- Added Cosmos command validator.
- Added Configuration entities for mapping application settings sections.
- Added Base request and responses classes.
- Added Response with continuation token for CosmosDb.
- Updated Mediatr dependency 8.0.1 to 8.0.2.

### Version 1.0.2

Bugfixes:

- Mediator extensions in Genzai.CosmosDb clear domain events list before sent it.

### Version 1.0.1

Changes:

- Fixed async methods with async fixer.
- Unit testing improovement in CosmosDb Library.
- Domain events support for CosmosDbLibrary.
  - Cosmos Entity with domain events list.
  - Event Dispatcher extension.
  - Repository handles event dispatching.
- Support Fluent Validation for Mediatr Commands.
- Base Cosmos Command with Id and Partition Key.

### Version 1.0.0

Initial version of Genzai Core:

- Azure CosmosDb repository and context.
- Entity Framework Core repository and context.
- Base Entities with domain events support.
