#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/movie/Microservice.Api.MovieService.Migrator/Microservice.Api.MovieService.Migrator.csproj", "microservices/movie/Microservice.Api.MovieService.Migrator/"]
COPY ["microservices/movie/Microservice.Infrastructure.MovieService/Microservice.Infrastructure.MovieService.csproj", "microservices/movie/Microservice.Infrastructure.MovieService/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/movie/Microservice.Domain.MovieService/Microservice.Domain.MovieService.csproj", "microservices/movie/Microservice.Domain.MovieService/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/movie/Microservice.Domain.MovieService.Shared/Microservice.Domain.MovieService.Shared.csproj", "microservices/movie/Microservice.Domain.MovieService.Shared/"]
RUN dotnet restore "microservices/movie/Microservice.Api.MovieService.Migrator/Microservice.Api.MovieService.Migrator.csproj"
COPY . .
WORKDIR "/src/microservices/movie/Microservice.Api.MovieService.Migrator"
RUN dotnet build "Microservice.Api.MovieService.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Api.MovieService.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Api.MovieService.Migrator.dll"]