> This project was created for educational purposes and to consolidate the acquired knowledge.
> 
> Keep in mind that the implementation is ongoing and some modules may work with bugs.

## About

This demo shows a working sample of microservices architecture using ASP.NET Core. It covers how to create microservices, how to create API gateways using Ocelot and how to deploy microservices using Docker containers.

## Diagram

![](images/diagram.png)

## Quick Start

The fastest and easiest way to run this project is a [Kubernates Cluster](#kubernates-cluster). You just need to install [Docker Desktop](https://docs.docker.com/desktop/windows/install/) and [Minikube](https://minikube.sigs.k8s.io/docs/start/) tools. Instead of building sources you can use latest version of images from public Docker Hub registry ([romanbilyak](https://hub.docker.com/u/romanbilyak)). Go to the folder [deploy](deploy) and run [run.ps1](deploy/run.ps1) script with specified registry parameter.

```powershell
.\run.ps1 -r 'romanbilyak'
```

Other methods of running this project are described below:

### Kubernates Cluster

Prerequisites:

- [.Net 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) (Included in [Visual Studio Community 2022 v17.4.4](https://visualstudio.microsoft.com/vs/community/))
- [Docker Desktop](https://docs.docker.com/desktop/windows/install/)
- [Minikube](https://minikube.sigs.k8s.io/docs/start/)
- [Kubernates](https://kubernetes.io/releases/download/)

Go to the folder [deploy](deploy) and run [run.ps1](deploy/run.ps1) or [run.cmd](deploy/run.cmd) script.

#### Examples

```powershell
.\run.ps1 -nodes 2 -cpus 4 -memory 4096 -r 'romanbilyak' -t 'latest'
```

| Param     | Short Form | Default  | Description                              |
| --------- | ---------- | -------- | ---------------------------------------- |
| -nodes    | -n         | 1        | Number of nodes.                         |
| -cpus     | -c         | 2        | Number of CPUs allocated to Kubernetes.  |
| -memory   | -m         | 2048     | Amount of RAM to allocate to Kubernetes. |
| -registry | -r         | ''       | Name of docker images registry.          |
| -tag      | -t         | 'latest' | Tag of images.                           |

The script will:

1. Start Kubernates Cluster localy using Minikube.

2. Build microservices images.

3. Push images to specified registry.
   
   > In a case when registry is not specified images will be pushed into Docker Deamon of cluster.
   
   > In a case when registry is set to 'localhost:5000' local registry inside cluster will be created.

4. Deploy services to local Kubernates Cluster.

5. Expose gateway service and start Kubernates Cluster dashboard.

Go to [https://localhost](https://localhost) to browse application.

### Docker Compose

Prerequisites:

- [.Net 7.0](https://dotnet.microsoft.com/en-us/download/dotnet/7.0) (Included in [Visual Studio Community 2022 v17.4.4](https://visualstudio.microsoft.com/vs/community/))

- [Docker Desktop](https://docs.docker.com/desktop/windows/install/)
1. Go to the folder [deploy\docker-compose-infrastructure](deploy/docker-compose-infrastructure) and run the following docker-compose command in CMD or PowerShell:
   
   ```
   docker-compose -f docker-compose.yml -f docker-compose.override.yml up
   ```

2. Go to the folder [deploy\docker-compose](deploy/docker-compose) and run the following docker-compose command in CMD or PowerShell:
   
   ```
   docker-compose -f docker-compose.yml -f docker-compose.override.yml up
   ```

### Visual Studio

Prerequisites:

- [Visual Studio Community 2022 (v17.4.4)](https://visualstudio.microsoft.com/vs/community/)
- [Docker Desktop](https://docs.docker.com/desktop/windows/install/)

Set **gateway** and **microservices** as startup projects. Your can also run the **docker-compose-infrastructure** project instead of installing and configuring infrastructure services by yourself.

![](images/multiple-startup-projects.png)

## Applications & Microservices

|                      | [Visual Studio](#visual-studio)                  | [Docker Compose](#docker-compose)                | [Kubernates Cluster](#kubernates-cluster)        |
| -------------------- | ------------------------------------------------ | ------------------------------------------------ | ------------------------------------------------ |
| app-angular          | [https://localhost:7001](https://localhost:7001) | [https://localhost:9001](https://localhost:9001) | [https://localhost](https://localhost)           |
| app-react            | [https://localhost:7002](https://localhost:7002) | [https://localhost:9002](https://localhost:9002) | [https://localhost/app](https://localhost/app)   |
| api-auth-service     | [https://localhost:7100](https://localhost:7100) | [https://localhost:9100](https://localhost:9100) | [https://localhost/auth](https://localhost/auth) |
| api-gateway-service  | [https://localhost:7200](https://localhost:7200) | [https://localhost:9200](https://localhost:9200) | [https://localhost/api](https://localhost/api)   |
| api-identity-service | [https://localhost:7201](https://localhost:7201) | [https://localhost:9201](https://localhost:9201) |                                                  |
| api-movie-service    | [https://localhost:7202](https://localhost:7202) | [https://localhost:9202](https://localhost:9202) |                                                  |
| api-review-service   | [https://localhost:7203](https://localhost:7203) | [https://localhost:9203](https://localhost:9203) |                                                  |
| api-payment-service  | [https://localhost:7204](https://localhost:7204) | [https://localhost:9204](https://localhost:9204) |                                                  |
| api-test-service     | [https://localhost:7205](https://localhost:7205) | [https://localhost:9205](https://localhost:9205) |                                                  |

## Project Structure

- **applications** - 

- **core** - 

- **deploy** -

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

- [x] [Swagger UI](https://swagger.io/tools/swagger-ui/)
- [x] [Ocelot API gateway](https://github.com/ThreeMammals/Ocelot)
- [ ] [Serilog](https://serilog.net/)
- [x] [CQRS pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
- [ ] [Event Sourcing pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/event-sourcing)
- [x] [MediatR](https://github.com/jbogard/MediatR)
- [x] [MassTransit](http://masstransit-project.com/)
- [x] [RabbitMQ](https://www.rabbitmq.com/)
