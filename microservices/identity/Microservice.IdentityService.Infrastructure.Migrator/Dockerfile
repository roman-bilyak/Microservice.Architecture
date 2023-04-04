#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/identity/Microservice.IdentityService.Infrastructure.Migrator/Microservice.IdentityService.Infrastructure.Migrator.csproj", "microservices/identity/Microservice.IdentityService.Infrastructure.Migrator/"]
COPY ["microservices/identity/Microservice.IdentityService.Infrastructure/Microservice.IdentityService.Infrastructure.csproj", "microservices/identity/Microservice.IdentityService.Infrastructure/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/identity/Microservice.IdentityService.Domain/Microservice.IdentityService.Domain.csproj", "microservices/identity/Microservice.IdentityService.Domain/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/identity/Microservice.IdentityService.Domain.Share/Microservice.IdentityService.Domain.Share.csproj", "microservices/identity/Microservice.IdentityService.Domain.Share/"]
RUN dotnet restore "microservices/identity/Microservice.IdentityService.Infrastructure.Migrator/Microservice.IdentityService.Infrastructure.Migrator.csproj"
COPY . .
WORKDIR "/src/microservices/identity/Microservice.IdentityService.Infrastructure.Migrator"
RUN dotnet build "Microservice.IdentityService.Infrastructure.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.IdentityService.Infrastructure.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.IdentityService.Infrastructure.Migrator.dll"]