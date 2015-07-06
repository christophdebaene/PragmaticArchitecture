# CleanArchitecture

This sample application illustrates a [Clean Architecture] (https://blog.8thlight.com/uncle-bob/2012/08/13/the-clean-architecture.html) with [CQRS] (http://martinfowler.com/bliki/CQRS.html).

| Project           | Description    |
| :---------------- | :------------- |
| **SlickBus** | | |
| SlickBus* | [SlickBus] (https://github.com/christophdebaene/CleanArchitecture/tree/master/src/SlickBus) is a very simple implementation of an in-process mediator pattern for request/response messaging. The pattern allows us to easily add cross-cutting concerns on the application layer through the [Decorator pattern] (https://en.wikipedia.org/wiki/Decorator_pattern). |
| SlickBus.SimpleInjector | Implements the `IMediator` interface through [SimpleInjector] (https://simpleinjector.org/index.html) |
| SlickBus.Contrib | To demonstrate that some handlers can be implemented independent of a project and can be reused. |
| **Command Layer** | |
| MyApp.Application*  | The application layer expresses use cases through commands and are the direct clients to the domain model. They are in control for transactions, authorization, logging, validation, etc. |
| MyApp.Application.Bootstrapper | This is the [Composition Root] (http://blog.ploeh.dk/2011/07/28/CompositionRoot/) of the application layer. In this project we have a reference to all needed frameworks to make the glue with our [Dependency Injection] (https://en.wikipedia.org/wiki/Dependency_injection) library.  |
| MyApp.Domain*  | Contains a rich domain model with behavior containing entities, value objects, repositories, etc. |
| MyApp.Domain.EntityFramework | Implements the repository pattern through [Entity Framework] (https://msdn.microsoft.com/en-us/data/ef.aspx)  |
| **Query Layer** | |
| MyApp.ReadModel* | Contains the [DTO's] (https://en.wikipedia.org/wiki/Data_transfer_object) driven by query use-case's |
| MyApp.ReadModel.Handlers | Implementation of the query use-case's to get the data through a thin data layer. Illustration through Entity Framework and [Dapper] (https://github.com/StackExchange/dapper-dot-net). |
| **UI Layer** | |
| MyApp.Web | [ASP.NET MVC] (http://www.asp.net/mvc) project using the `IMediator` inside the controller to process query and command messages. |

*The project may not contain references to infrastructure elements (database, UI, container, frameworks, etc.)

### Command/Query Separation

In this example to distinguish between a command or query, we simply use the namespace of the message where it resides.
Consequently we apply a different set of decorators. For the query part we don't need a a [Unit of Work] (http://martinfowler.com/eaaCatalog/unitOfWork.html) or Transactions for example.

#### Command Layer

```C#
var commandDecorators = new List<Type>
{
    typeof(UnitOfWorkHandler<,>),
    typeof(TransactionHandler<,>),
    typeof(ValidatorHandler<,>),
    typeof(LogHandler<,>)
};

container.RegisterRequestHandlerDecorators(commandDecorators, context =>
{
    var argument = context.ServiceType.GetGenericArguments()[0];
    return argument.Namespace.EndsWith("Commands");
});
```

#### Query Layer

```C#
var queryDecorators = new List<Type>
{
    typeof(ValidatorHandler<,>),
    typeof(StopwatchHandler<,>)
};

container.RegisterRequestHandlerDecorators(queryDecorators, context =>
{
    var argument = context.ServiceType.GetGenericArguments()[0];
    return argument.Namespace.EndsWith("Queries");
});
```
Another possibility is to have explicitly an `ICommand` and `IQuery` interface.

Some implementations of the mediator/bus pattern
* [MediatR] (https://github.com/jbogard/MediatR)
* [ShortBus] (https://github.com/mhinze/ShortBus)
* [CQRSlite] (https://github.com/gautema/CQRSlite/tree/master/Framework/CQRSlite)
* [Brighter] (https://github.com/iancooper/Paramore)
