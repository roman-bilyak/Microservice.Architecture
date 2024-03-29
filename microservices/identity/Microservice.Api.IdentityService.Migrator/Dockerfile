#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/identity/Microservice.Api.IdentityService.Migrator/Microservice.Api.IdentityService.Migrator.csproj", "microservices/identity/Microservice.Api.IdentityService.Migrator/"]
COPY ["microservices/identity/Microservice.Infrastructure.IdentityService/Microservice.Infrastructure.IdentityService.csproj", "microservices/identity/Microservice.Infrastructure.IdentityService/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/identity/Microservice.Domain.IdentityService/Microservice.Domain.IdentityService.csproj", "microservices/identity/Microservice.Domain.IdentityService/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/identity/Microservice.Domain.IdentityService.Shared/Microservice.Domain.IdentityService.Shared.csproj", "microservices/identity/Microservice.Domain.IdentityService.Shared/"]
RUN dotnet restore "microservices/identity/Microservice.Api.IdentityService.Migrator/Microservice.Api.IdentityService.Migrator.csproj"
COPY . .
WORKDIR "/src/microservices/identity/Microservice.Api.IdentityService.Migrator"
RUN dotnet build "Microservice.Api.IdentityService.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Api.IdentityService.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Api.IdentityService.Migrator.dll"]