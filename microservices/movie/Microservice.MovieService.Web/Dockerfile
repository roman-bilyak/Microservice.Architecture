#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microservices/movie/Microservice.MovieService.Web/Microservice.MovieService.Web.csproj", "microservices/movie/Microservice.MovieService.Web/"]
COPY ["core/Microservice.Api/Microservice.Api.csproj", "core/Microservice.Api/"]
COPY ["core/Microservice.AspNetCore.Authentication/Microservice.AspNetCore.Authentication.csproj", "core/Microservice.AspNetCore.Authentication/"]
COPY ["core/Microservice.AspNetCore/Microservice.AspNetCore.csproj", "core/Microservice.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["core/Microservice.AspNetCore.Authorization/Microservice.AspNetCore.Authorization.csproj", "core/Microservice.AspNetCore.Authorization/"]
COPY ["core/Microservice.AspNetCore.Swagger/Microservice.AspNetCore.Swagger.csproj", "core/Microservice.AspNetCore.Swagger/"]
COPY ["microservices/movie/Microservice.MovieService.Application/Microservice.MovieService.Application.csproj", "microservices/movie/Microservice.MovieService.Application/"]
COPY ["microservices/movie/Microservice.MovieService.Application.Contracts/Microservice.MovieService.Application.Contracts.csproj", "microservices/movie/Microservice.MovieService.Application.Contracts/"]
COPY ["core/Microservice.Application/Microservice.Application.csproj", "core/Microservice.Application/"]
COPY ["microservices/movie/Microservice.MovieService.Domain.Share/Microservice.MovieService.Domain.Share.csproj", "microservices/movie/Microservice.MovieService.Domain.Share/"]
COPY ["microservices/movie/Microservice.MovieService.Domain/Microservice.MovieService.Domain.csproj", "microservices/movie/Microservice.MovieService.Domain/"]
COPY ["core/Microservice.Database.Abstractions/Microservice.Database.Abstractions.csproj", "core/Microservice.Database.Abstractions/"]
COPY ["microservices/movie/Microservice.MovieService.Infrastructure/Microservice.MovieService.Infrastructure.csproj", "microservices/movie/Microservice.MovieService.Infrastructure/"]
COPY ["core/Microservice.Database.EntityFrameworkCore/Microservice.Database.EntityFrameworkCore.csproj", "core/Microservice.Database.EntityFrameworkCore/"]
RUN dotnet restore "microservices/movie/Microservice.MovieService.Web/Microservice.MovieService.Web.csproj"
COPY . .
WORKDIR "/src/microservices/movie/Microservice.MovieService.Web"
RUN dotnet build "Microservice.MovieService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.MovieService.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.MovieService.Web.dll"]