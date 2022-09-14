#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microservices/test/Microservice.TestService.Web/Microservice.TestService.Web.csproj", "microservices/test/Microservice.TestService.Web/"]
COPY ["core/Microservice.AspNetCore/Microservice.AspNetCore.csproj", "core/Microservice.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/test/Microservice.TestService.Infrastructure/Microservice.TestService.Infrastructure.csproj", "microservices/test/Microservice.TestService.Infrastructure/"]
COPY ["microservices/test/Microservice.TestService.Domain/Microservice.TestService.Domain.csproj", "microservices/test/Microservice.TestService.Domain/"]
COPY ["microservices/test/Microservice.TestService.Domain.Share/Microservice.TestService.Domain.Share.csproj", "microservices/test/Microservice.TestService.Domain.Share/"]
COPY ["core/Microservice.Database.Abstractions/Microservice.Database.Abstractions.csproj", "core/Microservice.Database.Abstractions/"]
COPY ["microservices/test/Microservice.TestService.Application/Microservice.TestService.Application.csproj", "microservices/test/Microservice.TestService.Application/"]
COPY ["microservices/test/Microservice.TestService.Application.Contracts/Microservice.TestService.Application.Contracts.csproj", "microservices/test/Microservice.TestService.Application.Contracts/"]
COPY ["core/Microservice.Application/Microservice.Application.csproj", "core/Microservice.Application/"]
RUN dotnet restore "microservices/test/Microservice.TestService.Web/Microservice.TestService.Web.csproj"
COPY . .
WORKDIR "/src/microservices/test/Microservice.TestService.Web"
RUN dotnet build "Microservice.TestService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.TestService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.TestService.Web.dll"]