#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/test/Microservice.TestService.Infrastructure.Migrator/Microservice.TestService.Infrastructure.Migrator.csproj", "microservices/test/Microservice.TestService.Infrastructure.Migrator/"]
COPY ["microservices/test/Microservice.TestService.Infrastructure/Microservice.TestService.Infrastructure.csproj", "microservices/test/Microservice.TestService.Infrastructure/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/test/Microservice.TestService.Domain/Microservice.TestService.Domain.csproj", "microservices/test/Microservice.TestService.Domain/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/test/Microservice.TestService.Domain.Share/Microservice.TestService.Domain.Share.csproj", "microservices/test/Microservice.TestService.Domain.Share/"]
RUN dotnet restore "microservices/test/Microservice.TestService.Infrastructure.Migrator/Microservice.TestService.Infrastructure.Migrator.csproj"
COPY . .
WORKDIR "/src/microservices/test/Microservice.TestService.Infrastructure.Migrator"
RUN dotnet build "Microservice.TestService.Infrastructure.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.TestService.Infrastructure.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.TestService.Infrastructure.Migrator.dll"]