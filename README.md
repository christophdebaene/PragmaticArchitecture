# CleanArchitecture

This sample application illustrates a [Clean Architecture](https://blog.8thlight.com/uncle-bob/2012/08/13/the-clean-architecture.html) with [CQRS](http://martinfowler.com/bliki/CQRS.html) and [DDD](https://en.wikipedia.org/wiki/Domain-driven_design) concepts.

## Overview

### Command Layer

**Application**

The application layer expresses use cases through commands and are the direct clients to the domain model. 
They are in control for transactions, authorization, logging, validation, etc.

Here we use a [Command Oriented Interface](https://martinfowler.com/bliki/CommandOrientedInterface.html) so that we can use the [Decorater](https://martinfowler.com/bliki/DecoratedCommand.html) pattern to easily add cross-cutting concerns,
conform to the [Open-closed principle](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle).

Use the [MediatR](https://github.com/jbogard/MediatR) library in combination with
 * [FluentValidation](https://github.com/FluentValidation/FluentValidation)
 * [Serilog](https://github.com/serilog/serilog)

**Domain**
 
Contains a rich domain model with behavior containing entities, value objects, repositories, etc.

**Bootstrapper** (Infrastructure)

This is the [Composition Root](http://blog.ploeh.dk/2011/07/28/CompositionRoot/) of the application layer. In this project we have a reference to all needed frameworks to make the glue with our [Dependency Injection](https://en.wikipedia.org/wiki/Dependency_injection) library.
 
### Query Layer

**ReadModel**

Contains the [DTO's](https://en.wikipedia.org/wiki/Data_transfer_object) driven by query use-case's
Implementation of the query use-case's to get the data through a thin data layer. Illustration through [EF Core](https://github.com/dotnet/efcore) and [Dapper](https://github.com/StackExchange/dapper-dot-net).
 
### UI Layer 

**Site**

[Razor Pages](https://docs.microsoft.com/en-us/aspnet/core/razor-pages) project using the `IMediator` to process query and command messages.
