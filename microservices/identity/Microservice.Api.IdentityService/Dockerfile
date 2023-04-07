#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/identity/Microservice.Api.IdentityService/Microservice.Api.IdentityService.csproj", "microservices/identity/Microservice.Api.IdentityService/"]
COPY ["core/Microservice.Api/Microservice.Api.csproj", "core/Microservice.Api/"]
COPY ["core/Microservice.Api.AspNetCore.Authentication/Microservice.Api.AspNetCore.Authentication.csproj", "core/Microservice.Api.AspNetCore.Authentication/"]
COPY ["core/Microservice.Api.AspNetCore/Microservice.Api.AspNetCore.csproj", "core/Microservice.Api.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["core/Microservice.Api.AspNetCore.Authorization/Microservice.Api.AspNetCore.Authorization.csproj", "core/Microservice.Api.AspNetCore.Authorization/"]
COPY ["core/Microservice.Api.AspNetCore.Swagger/Microservice.Api.AspNetCore.Swagger.csproj", "core/Microservice.Api.AspNetCore.Swagger/"]
COPY ["microservices/identity/Microservice.Application.IdentityService/Microservice.Application.IdentityService.csproj", "microservices/identity/Microservice.Application.IdentityService/"]
COPY ["core/Microservice.Application.CQRS/Microservice.Application.CQRS.csproj", "core/Microservice.Application.CQRS/"]
COPY ["core/Microservice.Application/Microservice.Application.csproj", "core/Microservice.Application/"]
COPY ["core/Microservice.Application.Abstractions/Microservice.Application.Abstractions.csproj", "core/Microservice.Application.Abstractions/"]
COPY ["microservices/identity/Microservice.Application.IdentityService.Abstractions/Microservice.Application.IdentityService.Abstractions.csproj", "microservices/identity/Microservice.Application.IdentityService.Abstractions/"]
COPY ["microservices/identity/Microservice.Domain.IdentityService.Shared/Microservice.Domain.IdentityService.Shared.csproj", "microservices/identity/Microservice.Domain.IdentityService.Shared/"]
COPY ["microservices/identity/Microservice.Domain.IdentityService/Microservice.Domain.IdentityService.csproj", "microservices/identity/Microservice.Domain.IdentityService/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/identity/Microservice.Infrastructure.IdentityService/Microservice.Infrastructure.IdentityService.csproj", "microservices/identity/Microservice.Infrastructure.IdentityService/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
RUN dotnet restore "microservices/identity/Microservice.Api.IdentityService/Microservice.Api.IdentityService.csproj"
COPY . .
WORKDIR "/src/microservices/identity/Microservice.Api.IdentityService"
RUN dotnet build "Microservice.Api.IdentityService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Api.IdentityService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Api.IdentityService.dll"]