> This project was created for educational purposes and to consolidate the acquired knowledge.
> 
> Keep in mind that the implementation is ongoing and some modules may work with bugs.

## About

This demo shows a working sample of microservices architecture using ASP.NET Core. It covers how to create microservices, how to create API gateways using Ocelot and how to deploy microservices using Docker containers.

## Diagram

![](images/diagram.png)

## Quick Start

The fastest and easiest way to run this project is a [Kubernates Cluster](#kubernates-cluster). You just need to install [Docker Desktop](https://docs.docker.com/desktop/windows/install/) and [Minikube](https://minikube.sigs.k8s.io/docs/start/) tools. Instead of building sources you can use latest version of images from public Docker Hub registry ([romanbilyak](https://hub.docker.com/u/romanbilyak)). Go to the folder [k8s](k8s) and run PowerShell script [run.ps1](k8s/run.ps1) with specified registry parameter.

```powershell
.\run.ps1 -r 'romanbilyak'
```

Go to https://localhost to browse application.

*Other methods of running this project are described below:*

### SSL certificate

SSL certificate for localhost domain signed by custom self-signed root certificate. To avoid browser warnings please install it ([ca.crt](ssl/ca.crt)) to the list of trusted root certification authorities.

### User

Name: **bob**

Password: **bob**

### Kubernates Cluster

Prerequisites:

- [.Net 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) (Included in [Visual Studio Community 2022 v17.4.4](https://visualstudio.microsoft.com/vs/community/))
- [Docker Desktop](https://docs.docker.com/desktop/windows/install/)
- [Minikube](https://minikube.sigs.k8s.io/docs/start/)
- [Kubernates](https://kubernetes.io/releases/download/)

Go to the folder [k8s](k8s) and run PowerShell script [run.ps1](k8s/run.ps1).

#### Examples

```powershell
.\run.ps1 -nodes 2 -cpus 4 -memory 4096 -r 'romanbilyak' -t 'latest'
```

| Param     | Short Form | Default | Description                              |
| --------- | ---------- | ------- | ---------------------------------------- |
| -nodes    | -n         | 1       | Number of nodes.                         |
| -cpus     | -c         | 2       | Number of CPUs allocated to Kubernetes.  |
| -memory   | -m         | 2048    | Amount of RAM to allocate to Kubernetes. |
| -registry | -r         | ''      | Name of docker images registry.          |
| -tag      | -t         | ''      | Tag of images.                           |

The script will:

1. Start Kubernates Cluster localy using Minikube.

2. Deploy services from specified images registry to local Kubernates Cluster.

3. Expose applications and api gateway services.

Go to [https://localhost](https://localhost) to browse application.

### Docker Compose

Prerequisites:

- [.Net 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) (Included in [Visual Studio Community 2022 v17.4.4](https://visualstudio.microsoft.com/vs/community/))

- [Docker Desktop](https://docs.docker.com/desktop/windows/install/)
1. Go to the folder [docker\docker-compose-infrastructure](docker/docker-compose-infrastructure) and run the following docker-compose command in PowerShell:
   
   ```powershell
   docker-compose -f docker-compose.yml -f docker-compose.override.yml up
   ```

2. Go to the folder [docker\docker-compose-api](docker/docker-compose-api) and run the following docker-compose command in PowerShell:
   
   ```powershell
   docker-compose -f docker-compose.yml -f docker-compose.override.yml up
   ```

3. Go to the folder [docker\docker-compose](docker/docker-compose) and run the following docker-compose command in PowerShell:
   
   ```powershell
   docker-compose -f docker-compose.yml -f docker-compose.override.yml up
   ```

### Visual Studio

Prerequisites:

- [Visual Studio Community 2022 (v17.4.4)](https://visualstudio.microsoft.com/vs/community/)
- [Docker Desktop](https://docs.docker.com/desktop/windows/install/)

Set **gateway** and **microservices** as startup projects. Your can also run the **docker-compose-infrastructure** project instead of installing and configuring infrastructure services by yourself.

![](images/multiple-startup-projects.png)

## Applications & Microservices

|                      | [Kubernates Cluster](#kubernates-cluster)          | [Docker Compose](#docker-compose)                | [Visual Studio](#visual-studio)                  |
| -------------------- | -------------------------------------------------- | ------------------------------------------------ | ------------------------------------------------ |
| app-angular          | [https://localhost](https://localhost)             | [https://localhost:9001](https://localhost:9001) | [https://localhost:7001](https://localhost:7001) |
| app-react            | [https://localhost/app/](https://localhost/app/)   | [https://localhost:9002](https://localhost:9002) | [https://localhost:7002](https://localhost:7002) |
| api-auth-service     | [https://localhost/auth/](https://localhost/auth/) | [https://localhost:9100](https://localhost:9100) | [https://localhost:7100](https://localhost:7100) |
| api-gateway-service  | [https://localhost/api/](https://localhost/api/)   | [https://localhost:9200](https://localhost:9200) | [https://localhost:7200](https://localhost:7200) |
| api-identity-service |                                                    | [https://localhost:9201](https://localhost:9201) | [https://localhost:7201](https://localhost:7201) |
| api-movie-service    |                                                    | [https://localhost:9202](https://localhost:9202) | [https://localhost:7202](https://localhost:7202) |
| api-review-service   |                                                    | [https://localhost:9203](https://localhost:9203) | [https://localhost:7203](https://localhost:7203) |
| api-payment-service  |                                                    | [https://localhost:9204](https://localhost:9204) | [https://localhost:7204](https://localhost:7204) |
| api-test-service     |                                                    | [https://localhost:9205](https://localhost:9205) | [https://localhost:7205](https://localhost:7205) |

## Project Structure

- **applications** - 

- **core** - 

- **docker** -

- **k8s** -

- **microservices** -

- **tests** -

## Layered Architecture

This project implements **NLayer architecture** and **Domain Driven Design**

### Shared Infrastructure Layer

- Database
  
  - EF Core

- Ast.Net Core

### Domain Layer

- Entities

- Specifications

- Validation Rules

- Managers

### Infrastructure Layer

- Database contexts

### Application Layer

- Application services

### Web Layer

Root layer for microservice's layers

- Controllers

- View Models

### Test Layer

- Unit Tests

## Technologies

- [ ] General
  - [ ] Version control
    - [ ] Git
    - [ ] GitHub
    - [ ] GitLab
  - [ ] Data structures
    - [ ] Non-generic
    - [ ] Generic
    - [ ] Thread-safe
  - [ ] Algorithms
- [ ] C#
  - [ ] C# 11
  - [ ] .Net 7
  - [ ] .Net CLI
- [ ] Architectural Patterns
  - [ ] MVC (Model-View-Controller)
  - [ ] MVVM (Model-View-ViewModel)
  - [ ] Microservices Architecture
  - [ ] RESTful API Design
  - [ ] Hexagonal Architecture
  - [ ] SOA (Service-Oriented Architecture)
  - [ ] EDA (Event-Driven Architecture)
  - [ ] Serverless Architecture
  - [ ] Layered Architecture
  - [ ] Clean Architecture
  - [ ] Multitier Architecture
  - [ ] Distributed Architecture
  - [ ] Serverless Architecture
  - [ ] [CQRS (Command Query Responsibility Segregation)](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
  - [ ] Dependency Injection
  - [ ] DDD (Domain-Driven Design)
  - [ ] Containerization and Orchestration
  - [ ] Gateway and Proxy Patterns
    - [ ] [Ocelot API Gateway](https://github.com/ThreeMammals/Ocelot)
  - [ ] [Event Sourcing](https://docs.microsoft.com/en-us/azure/architecture/patterns/event-sourcing)
  - [ ] Saga Pattern
- [ ] Design Patterns
  - [ ] Creational Patterns
  - [ ] Structural Patterns
  - [ ] Behavioral Patterns
- [ ] Design Principles
  - [ ] SOLID Principles
    - [ ] Single Responsibility Principle
    - [ ] Open-Closed Principle
    - [ ] Liskov Substitution Principle
    - [ ] Interface Segregation Principle
    - [ ] Dependency Inversion Principle
  - [ ] DRY (Don't Repeat Yourself)
  - [ ] YAGNI (You Ain't Gonna Need It)
  - [ ] KISS (Keep It Simple, Stupid)
- [ ] Asp.Net Core
  - [ ] Web API
  - [ ] Minimal APIs
  - [ ] Routing
  - [ ] Middlewares
  - [ ] Filters & Attributes
  - [ ] Configuration
  - [ ] Authentication & Authorization
    - [ ] JWT (JSON Web Tokens)
    - [ ] OAuth2 and OpenID Connect
    - [ ] Identity Server
  - [ ] CORS (Cross-Origin Resource Sharing)
  - [ ] Dependency Injection
- [ ] APIs
  - [ ] Protocols
    - [ ] SOAP (Simple object access protocol)
    - [ ] REST (Representational state transfer)
      - [ ] Web API
      - [ ] Minimal APIs
      - [ ] FastEndpoints
    - [ ] gRPC (Google remote procedure call)
    - [ ] GraphQL (Graph query language)
      - [ ] HotChocolate
    - [ ] OData (Open Data Protocol)
  - [ ] Documentation
    - [ ] OpenAPI/Swagger
    - [ ] AsyncAPI
  - [ ] SDK Libraries
    - [ ] Refit
    - [ ] RestSharp
    - [ ] Flurt
- [ ] RDBMS Databases
  - [ ] SQL Syntax
  - [ ] Stored Procedures
  - [ ] Databases
    - [ ] SQL Server
    - [ ] Postgress
    - [ ] MySql/MariaDB
- [ ] NoSQL Databases
  - [ ] Cloud proprietary
    - [ ] Azure CosmosDB
    - [ ] AWS DynamoDB
  - [ ] ElasticSearch
  - [ ] Redis
  - [ ] MongoDB
- [ ] ORMs
  - [ ] [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
    - [ ] Code first & Migrations
    - [ ] Change Tracker API
    - [ ] Lazy Loading, Eager Loading, Explicit Loading
  - [ ] Dapper
- [ ] Dependency Injection
  - [ ] Microsoft Dependency Injection
  - [ ] DryIoc
  - [ ] Scrutor
  - [ ] Castle Windsor 
- [ ] Caching
  - [ ] Output Caching
  - [ ] Response Caching
  - [ ] Redis
- [ ] Logging
  - [ ] Microsoft Logging
  - [ ] NLogger
  - [ ] [Serilog](https://serilog.net/)
- [ ] Messaging
  - [ ] Azure Service Bus
  - [ ] AWS SQS/SNS
  - [ ] [RabbitMQ](https://www.rabbitmq.com/)
  - [ ] [MassTransit](http://masstransit-project.com/)
- [ ] Streaming
  - [ ] Apache Kafka
  - [ ] AWS Kinesis
  - [ ] Azure Event Hubs
- [ ] Real-Time Communication
  - [ ] SignalR
  - [ ] Web Sockets
- [ ] Task Scheduling
  - [ ] BackgroundService
  - [ ] PeriodicTimer
  - [ ] HangFire
- [ ] Testing
  - [ ] Unit Testing
    - [ ] Frameworks
      - [ ] xUnit
      - [ ] NUnit
    - [ ] Mocking
      - [ ] NSubstitute
      - [ ] Moq
    - [ ] Assertion
      - [ ] FluentAssertions
    - [ ] Test Data Generators
      - [ ] Bogus
      - [ ] AutiFixture
  - [ ] Integration Testing
    - [ ] WebApplicationFactory
    - [ ] Respawn
    - [ ] Docker
      - [ ] Testcontainers
  - [ ] Snapshot Testing
    - [ ] Verify
  - [ ] E2E Testing
    - [ ] Playwright
  - [ ] Performance Testing
    - [ ] K6
    - [ ] MBomber
    - [ ] JMeter
- [ ] Monitoring and Telemetry
  - [ ] OpenTelemetry
    - [ ] Jeager
  - [ ] Prometheus
  - [ ] Grafana
  - [ ] ELK Stack
  - [ ] Datadog
- [ ] Containers
  - [ ] Containerization
    - [ ] Docker
    - [ ] Podman
  - [ ] Orchestration
    - [ ] Kubernetes
- [ ] Cloud
  - [ ] Providers
    - [ ] Azure
    - [ ] AWS
    - [ ] Google Cloud Platform
  - [ ] Serverless
    - [ ] Azure Functions
    - [ ] AWS Lambda
  - [ ] File Store
    - [ ] Azure Store
    - [ ] AWS S3
- [ ] CI/CD
  - [ ] GitHub Actions
  - [ ] TeamCity
  - [ ] Octopus Deploy
  - [ ] Azure Pipelines
  - [ ] Gitlab CI
  - [ ] Jenkins
  - [ ] Build Automation
    - [ ] Cake
    - [ ] Nuke
- [ ] DevOps
  - [ ] Infrastructure as code
    - [ ] Terraform
    - [ ] Pulumi
- [ ] .Net Libraries
  - [ ] Polly
  - [ ] FluentValidation
  - [ ] Humanizer.Core
  - [ ] Benchmark.Net
  - [ ] [MediatR](https://github.com/jbogard/MediatR)
  - [ ] Units.NET
  - [ ] NodeTime

## Contributing

Contributions to this repository are welcome. If you have a code sample or educational resource that you would like to contribute, please follow these steps:

1. Fork the repository.
2. Create a new branch for your changes.
3. Make your changes and commit them to your branch.
4. Submit a pull request.

Please ensure that your contributions adhere to the repository's code of conduct and that they are well-documented and follow best practices.

## License

This repository is licensed under the MIT License. See the [LICENSE](LICENSE) file for more information.
