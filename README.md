# Clean Tickets

Clean Tickets is a simple event ticketing system built on ASP.NET Core 6, Entity Framework Core and using Minimal API.

The application is designed to be a small service working together with other components through events. Even though there is no actual implementation of any messaging system in place right now, it is intended to be event-driven.

# Architecture

This project follows the Clean Architecture patterns with some slight changes. It implements Command and Query Separation by implementing disparate `ICommand` and `IQuery` interfaces and handlers. While it does currently use a mediator to mediate between incoming messages and their handlers, it should be fairly easily changed to a non-mediated broker.

Another departure from common Clean Architecture implementations is the fact that the domain repositories are contained in the domain assembly. A good number of implementations place these in the Application layer instead, but I feel that the domain models, as well as the repositories that abstract their interactions should be in the domain layer. I'm still on the fence about this one though.

## Structure

The solution consists of four projects:

* **Application** contains the use cases and features
* **Domain** contains the domain models (entities)
* **Infrastructure.Persistence** contains the Entity Framework persistence code
* **Api** contains an implementation of a front-end for the application layer, supporting its basic operations using Minimal API

## Tests

The solution also includes a test project for the various command and queries implemented by the application layer.
