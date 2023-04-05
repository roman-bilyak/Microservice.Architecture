#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/review/Microservice.ReviewService.Migrator/Microservice.ReviewService.Migrator.csproj", "microservices/review/Microservice.ReviewService.Migrator/"]
COPY ["microservices/review/Microservice.ReviewService.Infrastructure/Microservice.ReviewService.Infrastructure.csproj", "microservices/review/Microservice.ReviewService.Infrastructure/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/review/Microservice.ReviewService.Domain/Microservice.ReviewService.Domain.csproj", "microservices/review/Microservice.ReviewService.Domain/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/review/Microservice.ReviewService.Domain.Share/Microservice.ReviewService.Domain.Share.csproj", "microservices/review/Microservice.ReviewService.Domain.Share/"]
RUN dotnet restore "microservices/review/Microservice.ReviewService.Migrator/Microservice.ReviewService.Migrator.csproj"
COPY . .
WORKDIR "/src/microservices/review/Microservice.ReviewService.Migrator"
RUN dotnet build "Microservice.ReviewService.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.ReviewService.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.ReviewService.Migrator.dll"]