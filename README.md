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

*Means that the project should be independent of infrastructure (Database, UI, Container, Frameworks, etc.)
