#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/review/Microservice.Api.ReviewService/Microservice.Api.ReviewService.csproj", "microservices/review/Microservice.Api.ReviewService/"]
COPY ["core/Microservice.Api/Microservice.Api.csproj", "core/Microservice.Api/"]
COPY ["core/Microservice.Api.AspNetCore.Authentication/Microservice.Api.AspNetCore.Authentication.csproj", "core/Microservice.Api.AspNetCore.Authentication/"]
COPY ["core/Microservice.Api.AspNetCore/Microservice.Api.AspNetCore.csproj", "core/Microservice.Api.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["core/Microservice.Api.AspNetCore.Authorization/Microservice.Api.AspNetCore.Authorization.csproj", "core/Microservice.Api.AspNetCore.Authorization/"]
COPY ["core/Microservice.Api.AspNetCore.Swagger/Microservice.Api.AspNetCore.Swagger.csproj", "core/Microservice.Api.AspNetCore.Swagger/"]
COPY ["microservices/review/Microservice.Application.ReviewService/Microservice.Application.ReviewService.csproj", "microservices/review/Microservice.Application.ReviewService/"]
COPY ["core/Microservice.Application.CQRS/Microservice.Application.CQRS.csproj", "core/Microservice.Application.CQRS/"]
COPY ["core/Microservice.Application/Microservice.Application.csproj", "core/Microservice.Application/"]
COPY ["core/Microservice.Application.Abstractions/Microservice.Application.Abstractions.csproj", "core/Microservice.Application.Abstractions/"]
COPY ["microservices/review/Microservice.Application.ReviewService.Abstractions/Microservice.Application.ReviewService.Abstractions.csproj", "microservices/review/Microservice.Application.ReviewService.Abstractions/"]
COPY ["microservices/review/Microservice.Domain.ReviewService.Shared/Microservice.Domain.ReviewService.Shared.csproj", "microservices/review/Microservice.Domain.ReviewService.Shared/"]
COPY ["microservices/review/Microservice.Domain.ReviewService/Microservice.Domain.ReviewService.csproj", "microservices/review/Microservice.Domain.ReviewService/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/review/Microservice.Infrastructure.ReviewService/Microservice.Infrastructure.ReviewService.csproj", "microservices/review/Microservice.Infrastructure.ReviewService/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
RUN dotnet restore "microservices/review/Microservice.Api.ReviewService/Microservice.Api.ReviewService.csproj"
COPY . .
WORKDIR "/src/microservices/review/Microservice.Api.ReviewService"
RUN dotnet build "Microservice.Api.ReviewService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Api.ReviewService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Api.ReviewService.dll"]