#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["applications/Microservice.Api.AuthService/Microservice.Api.AuthService.csproj", "applications/Microservice.Api.AuthService/"]
COPY ["core/Microservice.Api.AspNetCore/Microservice.Api.AspNetCore.csproj", "core/Microservice.Api.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/identity/Microservice.Infrastructure.IdentityService/Microservice.Infrastructure.IdentityService.csproj", "microservices/identity/Microservice.Infrastructure.IdentityService/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/identity/Microservice.Domain.IdentityService/Microservice.Domain.IdentityService.csproj", "microservices/identity/Microservice.Domain.IdentityService/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["microservices/identity/Microservice.Domain.IdentityService.Shared/Microservice.Domain.IdentityService.Shared.csproj", "microservices/identity/Microservice.Domain.IdentityService.Shared/"]
RUN dotnet restore "applications/Microservice.Api.AuthService/Microservice.Api.AuthService.csproj"
COPY . .
WORKDIR "/src/applications/Microservice.Api.AuthService"
RUN dotnet build "Microservice.Api.AuthService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Api.AuthService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Api.AuthService.dll"]