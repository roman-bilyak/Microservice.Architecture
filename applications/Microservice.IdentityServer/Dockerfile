#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["applications/Microservice.IdentityServer/Microservice.IdentityServer.csproj", "applications/Microservice.IdentityServer/"]
COPY ["core/Microservice.AspNetCore/Microservice.AspNetCore.csproj", "core/Microservice.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/identity/Microservice.IdentityService.Infrastructure/Microservice.IdentityService.Infrastructure.csproj", "microservices/identity/Microservice.IdentityService.Infrastructure/"]
COPY ["core/Microservice.Database.EntityFrameworkCore/Microservice.Database.EntityFrameworkCore.csproj", "core/Microservice.Database.EntityFrameworkCore/"]
COPY ["core/Microservice.Database.Abstractions/Microservice.Database.Abstractions.csproj", "core/Microservice.Database.Abstractions/"]
COPY ["microservices/identity/Microservice.IdentityService.Domain/Microservice.IdentityService.Domain.csproj", "microservices/identity/Microservice.IdentityService.Domain/"]
COPY ["microservices/identity/Microservice.IdentityService.Domain.Share/Microservice.IdentityService.Domain.Share.csproj", "microservices/identity/Microservice.IdentityService.Domain.Share/"]
RUN dotnet restore "applications/Microservice.IdentityServer/Microservice.IdentityServer.csproj"
COPY . .
WORKDIR "/src/applications/Microservice.IdentityServer"
RUN dotnet build "Microservice.IdentityServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.IdentityServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.IdentityServer.dll"]