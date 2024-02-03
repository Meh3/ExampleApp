## Before starting:
I have used SQL Server as storage for this sample application, so you will need to provide a connection string for this application to work.

## Assumptions and decisions:
- Create a separate module for truck management using a modular monolithic approach (always start with this, unless you have a very good reason to go with microservices).
- Use Clean Architecture with DDD and CQRS (only as much as needed for the example application).
- Maybe some layers (like application or domain services) seem a bit overhead for this example app, but in any real case scenario there is some logic added in each layer. In general, I'm also happy to use Vertical Slices architecture instead of Clean Architecture for not complicated cases where layers are not necessarily separated and abstracted away.
- Use of minimal API
- OData is used to read data.
- omitted: logging, authentication, docker, testing (I would test this via WebApplicationFactory and maybe some unit tests for the domain), etc.
- For simplicity, no concept of user.
