#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microservices/review/Microservice.ReviewService.Api/Microservice.ReviewService.Api.csproj", "microservices/review/Microservice.ReviewService.Api/"]
COPY ["core/Microservice.Api/Microservice.Api.csproj", "core/Microservice.Api/"]
COPY ["core/Microservice.AspNetCore.Authentication/Microservice.AspNetCore.Authentication.csproj", "core/Microservice.AspNetCore.Authentication/"]
COPY ["core/Microservice.AspNetCore/Microservice.AspNetCore.csproj", "core/Microservice.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["core/Microservice.AspNetCore.Authorization/Microservice.AspNetCore.Authorization.csproj", "core/Microservice.AspNetCore.Authorization/"]
COPY ["core/Microservice.AspNetCore.Swagger/Microservice.AspNetCore.Swagger.csproj", "core/Microservice.AspNetCore.Swagger/"]
COPY ["microservices/review/Microservice.ReviewService.Application/Microservice.ReviewService.Application.csproj", "microservices/review/Microservice.ReviewService.Application/"]
COPY ["core/Microservice.CQRS/Microservice.CQRS.csproj", "core/Microservice.CQRS/"]
COPY ["microservices/review/Microservice.ReviewService.Application.Abstractions/Microservice.ReviewService.Application.Abstractions.csproj", "microservices/review/Microservice.ReviewService.Application.Abstractions/"]
COPY ["core/Microservice.Application.Abstractions/Microservice.Application.Abstractions.csproj", "core/Microservice.Application.Abstractions/"]
COPY ["microservices/review/Microservice.ReviewService.Domain.Share/Microservice.ReviewService.Domain.Share.csproj", "microservices/review/Microservice.ReviewService.Domain.Share/"]
COPY ["microservices/review/Microservice.ReviewService.Domain/Microservice.ReviewService.Domain.csproj", "microservices/review/Microservice.ReviewService.Domain/"]
COPY ["core/Microservice.Database.Abstractions/Microservice.Database.Abstractions.csproj", "core/Microservice.Database.Abstractions/"]
COPY ["microservices/review/Microservice.ReviewService.Infrastructure/Microservice.ReviewService.Infrastructure.csproj", "microservices/review/Microservice.ReviewService.Infrastructure/"]
COPY ["core/Microservice.Database.EntityFrameworkCore/Microservice.Database.EntityFrameworkCore.csproj", "core/Microservice.Database.EntityFrameworkCore/"]
RUN dotnet restore "microservices/review/Microservice.ReviewService.Api/Microservice.ReviewService.Api.csproj"
COPY . .
WORKDIR "/src/microservices/review/Microservice.ReviewService.Api"
RUN dotnet build "Microservice.ReviewService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.ReviewService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.ReviewService.Api.dll"]