#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microservices/movie/Microservice.MovieService.Web/Microservice.MovieService.Web.csproj", "microservices/movie/Microservice.MovieService.Web/"]
COPY ["microservices/movie/Microservice.MovieService.Infrastructure/Microservice.MovieService.Infrastructure.csproj", "microservices/movie/Microservice.MovieService.Infrastructure/"]
COPY ["microservices/movie/Microservice.MovieService.Domain/Microservice.MovieService.Domain.csproj", "microservices/movie/Microservice.MovieService.Domain/"]
COPY ["microservices/movie/Microservice.MovieService.Domain.Share/Microservice.MovieService.Domain.Share.csproj", "microservices/movie/Microservice.MovieService.Domain.Share/"]
COPY ["core/Microservice.Core.Database/Microservice.Core.Database.csproj", "core/Microservice.Core.Database/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["infrastructure/Microservice.Infrastructure.Database.EntityFrameworkCore/Microservice.Infrastructure.Database.EntityFrameworkCore.csproj", "infrastructure/Microservice.Infrastructure.Database.EntityFrameworkCore/"]
COPY ["microservices/movie/Microservice.MovieService.Application/Microservice.MovieService.Application.csproj", "microservices/movie/Microservice.MovieService.Application/"]
COPY ["microservices/movie/Microservice.MovieService.Application.Contracts/Microservice.MovieService.Application.Contracts.csproj", "microservices/movie/Microservice.MovieService.Application.Contracts/"]
COPY ["core/Microservice.Application/Microservice.Application.csproj", "core/Microservice.Application/"]
COPY ["infrastructure/Microservice.Infrastructure.AspNetCore/Microservice.Infrastructure.AspNetCore.csproj", "infrastructure/Microservice.Infrastructure.AspNetCore/"]
RUN dotnet restore "microservices/movie/Microservice.MovieService.Web/Microservice.MovieService.Web.csproj"
COPY . .
WORKDIR "/src/microservices/movie/Microservice.MovieService.Web"
RUN dotnet build "Microservice.MovieService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.MovieService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.MovieService.Web.dll"]