#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["microservices/review/Microservice.Api.ReviewService.Migrator/Microservice.Api.ReviewService.Migrator.csproj", "microservices/review/Microservice.Api.ReviewService.Migrator/"]
COPY ["microservices/review/Microservice.Infrastructure.ReviewService/Microservice.Infrastructure.ReviewService.csproj", "microservices/review/Microservice.Infrastructure.ReviewService/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/review/Microservice.Domain.ReviewService/Microservice.Domain.ReviewService.csproj", "microservices/review/Microservice.Domain.ReviewService/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/review/Microservice.Domain.ReviewService.Shared/Microservice.Domain.ReviewService.Shared.csproj", "microservices/review/Microservice.Domain.ReviewService.Shared/"]
RUN dotnet restore "microservices/review/Microservice.Api.ReviewService.Migrator/Microservice.Api.ReviewService.Migrator.csproj"
COPY . .
WORKDIR "/src/microservices/review/Microservice.Api.ReviewService.Migrator"
RUN dotnet build "Microservice.Api.ReviewService.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Api.ReviewService.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Api.ReviewService.Migrator.dll"]