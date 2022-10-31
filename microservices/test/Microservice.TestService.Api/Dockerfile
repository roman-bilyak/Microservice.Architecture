#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microservices/test/Microservice.TestService.Api/Microservice.TestService.Api.csproj", "microservices/test/Microservice.TestService.Api/"]
COPY ["core/Microservice.Api/Microservice.Api.csproj", "core/Microservice.Api/"]
COPY ["core/Microservice.AspNetCore.Authentication/Microservice.AspNetCore.Authentication.csproj", "core/Microservice.AspNetCore.Authentication/"]
COPY ["core/Microservice.AspNetCore/Microservice.AspNetCore.csproj", "core/Microservice.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["core/Microservice.AspNetCore.Authorization/Microservice.AspNetCore.Authorization.csproj", "core/Microservice.AspNetCore.Authorization/"]
COPY ["core/Microservice.AspNetCore.Swagger/Microservice.AspNetCore.Swagger.csproj", "core/Microservice.AspNetCore.Swagger/"]
COPY ["microservices/test/Microservice.TestService.Application/Microservice.TestService.Application.csproj", "microservices/test/Microservice.TestService.Application/"]
COPY ["microservices/test/Microservice.TestService.Application.Abstractions/Microservice.TestService.Application.Abstrations.csproj", "microservices/test/Microservice.TestService.Application.Abstractions/"]
COPY ["core/Microservice.Application.Abstractions/Microservice.Application.Abstractions.csproj", "core/Microservice.Application.Abstractions/"]
COPY ["microservices/test/Microservice.TestService.Domain.Share/Microservice.TestService.Domain.Share.csproj", "microservices/test/Microservice.TestService.Domain.Share/"]
COPY ["microservices/test/Microservice.TestService.Domain/Microservice.TestService.Domain.csproj", "microservices/test/Microservice.TestService.Domain/"]
COPY ["core/Microservice.Database.Abstractions/Microservice.Database.Abstractions.csproj", "core/Microservice.Database.Abstractions/"]
COPY ["microservices/test/Microservice.TestService.Infrastructure/Microservice.TestService.Infrastructure.csproj", "microservices/test/Microservice.TestService.Infrastructure/"]
RUN dotnet restore "microservices/test/Microservice.TestService.Api/Microservice.TestService.Api.csproj"
COPY . .
WORKDIR "/src/microservices/test/Microservice.TestService.Api"
RUN dotnet build "Microservice.TestService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.TestService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.TestService.Api.dll"]