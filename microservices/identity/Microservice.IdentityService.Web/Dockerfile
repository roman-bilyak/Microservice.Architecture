#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microservices/identity/Microservice.IdentityService.Web/Microservice.IdentityService.Web.csproj", "microservices/identity/Microservice.IdentityService.Web/"]
COPY ["core/Microservice.AspNetCore/Microservice.AspNetCore.csproj", "core/Microservice.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/identity/Microservice.IdentityService.Application/Microservice.IdentityService.Application.csproj", "microservices/identity/Microservice.IdentityService.Application/"]
COPY ["microservices/identity/Microservice.IdentityService.Domain/Microservice.IdentityService.Domain.csproj", "microservices/identity/Microservice.IdentityService.Domain/"]
COPY ["microservices/identity/Microservice.IdentityService.Domain.Share/Microservice.IdentityService.Domain.Share.csproj", "microservices/identity/Microservice.IdentityService.Domain.Share/"]
COPY ["core/Microservice.Database.Abstractions/Microservice.Database.Abstractions.csproj", "core/Microservice.Database.Abstractions/"]
COPY ["microservices/identity/Microservice.IdentityService.Application.Contracts/Microservice.IdentityService.Application.Contracts.csproj", "microservices/identity/Microservice.IdentityService.Application.Contracts/"]
COPY ["core/Microservice.Application/Microservice.Application.csproj", "core/Microservice.Application/"]
COPY ["microservices/identity/Microservice.IdentityService.Infrastructure/Microservice.IdentityService.Infrastructure.csproj", "microservices/identity/Microservice.IdentityService.Infrastructure/"]
COPY ["core/Microservice.Database.EntityFrameworkCore/Microservice.Database.EntityFrameworkCore.csproj", "core/Microservice.Database.EntityFrameworkCore/"]
COPY ["core/Microservice.AspNetCore.Swagger/Microservice.AspNetCore.Swagger.csproj", "core/Microservice.AspNetCore.Swagger/"]
RUN dotnet restore "microservices/identity/Microservice.IdentityService.Web/Microservice.IdentityService.Web.csproj"
COPY . .
WORKDIR "/src/microservices/identity/Microservice.IdentityService.Web"
RUN dotnet build "Microservice.IdentityService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.IdentityService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.IdentityService.Web.dll"]