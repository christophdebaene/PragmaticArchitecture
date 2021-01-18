# CleanArchitecture

This sample application illustrates a [Clean Architecture](https://blog.8thlight.com/uncle-bob/2012/08/13/the-clean-architecture.html) with [CQRS](http://martinfowler.com/bliki/CQRS.html).

| Project           | Description    |
| :---------------- | :------------- |
| **Command Layer** | |
| MyApp.Application  | The application layer expresses use cases through commands and are the direct clients to the domain model. They are in control for transactions, authorization, logging, validation, etc. |
| MyApp.Application.Bootstrapper | This is the [Composition Root](http://blog.ploeh.dk/2011/07/28/CompositionRoot/) of the application layer. In this project we have a reference to all needed frameworks to make the glue with our [Dependency Injection](https://en.wikipedia.org/wiki/Dependency_injection) library.  |
| MyApp.Domain  | Contains a rich domain model with behavior containing entities, value objects, repositories, etc. |
| **Query Layer** | |
| MyApp.ReadModel | Contains the [DTO's](https://en.wikipedia.org/wiki/Data_transfer_object) driven by query use-case's |
| MyApp.ReadModel.Handlers | Implementation of the query use-case's to get the data through a thin data layer. Illustration through Entity Framework and [Dapper](https://github.com/StackExchange/dapper-dot-net). |
| **UI Layer** | |
| MyApp.Site | [Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages) project using the `IMediator` to process query and command messages. |
