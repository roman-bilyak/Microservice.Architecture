#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/movie/Microservice.MovieService.Migrator/Microservice.MovieService.Migrator.csproj", "microservices/movie/Microservice.MovieService.Migrator/"]
COPY ["microservices/movie/Microservice.MovieService.Infrastructure/Microservice.MovieService.Infrastructure.csproj", "microservices/movie/Microservice.MovieService.Infrastructure/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/movie/Microservice.MovieService.Domain/Microservice.MovieService.Domain.csproj", "microservices/movie/Microservice.MovieService.Domain/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/movie/Microservice.MovieService.Domain.Share/Microservice.MovieService.Domain.Share.csproj", "microservices/movie/Microservice.MovieService.Domain.Share/"]
RUN dotnet restore "microservices/movie/Microservice.MovieService.Migrator/Microservice.MovieService.Migrator.csproj"
COPY . .
WORKDIR "/src/microservices/movie/Microservice.MovieService.Migrator"
RUN dotnet build "Microservice.MovieService.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.MovieService.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.MovieService.Migrator.dll"]