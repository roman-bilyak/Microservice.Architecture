#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/identity/Microservice.IdentityService.Api/Microservice.IdentityService.Api.csproj", "microservices/identity/Microservice.IdentityService.Api/"]
COPY ["core/Microservice.Api/Microservice.Api.csproj", "core/Microservice.Api/"]
COPY ["core/Microservice.Api.AspNetCore.Authentication/Microservice.Api.AspNetCore.Authentication.csproj", "core/Microservice.Api.AspNetCore.Authentication/"]
COPY ["core/Microservice.Api.AspNetCore/Microservice.Api.AspNetCore.csproj", "core/Microservice.Api.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["core/Microservice.Api.AspNetCore.Authorization/Microservice.Api.AspNetCore.Authorization.csproj", "core/Microservice.Api.AspNetCore.Authorization/"]
COPY ["core/Microservice.Api.AspNetCore.Swagger/Microservice.Api.AspNetCore.Swagger.csproj", "core/Microservice.Api.AspNetCore.Swagger/"]
COPY ["microservices/identity/Microservice.IdentityService.Application/Microservice.IdentityService.Application.csproj", "microservices/identity/Microservice.IdentityService.Application/"]
COPY ["core/Microservice.Application.CQRS/Microservice.Application.CQRS.csproj", "core/Microservice.Application.CQRS/"]
COPY ["core/Microservice.Application/Microservice.Application.csproj", "core/Microservice.Application/"]
COPY ["core/Microservice.Application.Abstractions/Microservice.Application.Abstractions.csproj", "core/Microservice.Application.Abstractions/"]
COPY ["microservices/identity/Microservice.IdentityService.Application.Abstractions/Microservice.IdentityService.Application.Abstractions.csproj", "microservices/identity/Microservice.IdentityService.Application.Abstractions/"]
COPY ["microservices/identity/Microservice.IdentityService.Domain.Share/Microservice.IdentityService.Domain.Share.csproj", "microservices/identity/Microservice.IdentityService.Domain.Share/"]
COPY ["microservices/identity/Microservice.IdentityService.Domain/Microservice.IdentityService.Domain.csproj", "microservices/identity/Microservice.IdentityService.Domain/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/identity/Microservice.IdentityService.Infrastructure/Microservice.IdentityService.Infrastructure.csproj", "microservices/identity/Microservice.IdentityService.Infrastructure/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
RUN dotnet restore "microservices/identity/Microservice.IdentityService.Api/Microservice.IdentityService.Api.csproj"
COPY . .
WORKDIR "/src/microservices/identity/Microservice.IdentityService.Api"
RUN dotnet build "Microservice.IdentityService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.IdentityService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.IdentityService.Api.dll"]