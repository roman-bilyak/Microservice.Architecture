#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/test/Microservice.Api.TestService.Migrator/Microservice.Api.TestService.Migrator.csproj", "microservices/test/Microservice.Api.TestService.Migrator/"]
COPY ["microservices/test/Microservice.Infrastructure.TestService/Microservice.Infrastructure.TestService.csproj", "microservices/test/Microservice.Infrastructure.TestService/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/test/Microservice.Domain.TestService/Microservice.Domain.TestService.csproj", "microservices/test/Microservice.Domain.TestService/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/test/Microservice.Domain.TestService.Shared/Microservice.Domain.TestService.Shared.csproj", "microservices/test/Microservice.Domain.TestService.Shared/"]
RUN dotnet restore "microservices/test/Microservice.Api.TestService.Migrator/Microservice.Api.TestService.Migrator.csproj"
COPY . .
WORKDIR "/src/microservices/test/Microservice.Api.TestService.Migrator"
RUN dotnet build "Microservice.Api.TestService.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Api.TestService.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Api.TestService.Migrator.dll"]