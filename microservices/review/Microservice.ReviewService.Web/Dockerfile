#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microservices/review/Microservice.ReviewService.Web/Microservice.ReviewService.Web.csproj", "microservices/review/Microservice.ReviewService.Web/"]
COPY ["microservices/review/Microservice.ReviewService.Infrastructure/Microservice.ReviewService.Infrastructure.csproj", "microservices/review/Microservice.ReviewService.Infrastructure/"]
COPY ["infrastructure/Microservice.Infrastructure.Database.EntityFrameworkCore/Microservice.Infrastructure.Database.EntityFrameworkCore.csproj", "infrastructure/Microservice.Infrastructure.Database.EntityFrameworkCore/"]
COPY ["core/Microservice.Core.Database/Microservice.Core.Database.csproj", "core/Microservice.Core.Database/"]
COPY ["microservices/review/Microservice.ReviewService.Domain/Microservice.ReviewService.Domain.csproj", "microservices/review/Microservice.ReviewService.Domain/"]
COPY ["microservices/review/Microservice.ReviewService.Domain.Share/Microservice.ReviewService.Domain.Share.csproj", "microservices/review/Microservice.ReviewService.Domain.Share/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/review/Microservice.ReviewService.Application/Microservice.ReviewService.Application.csproj", "microservices/review/Microservice.ReviewService.Application/"]
COPY ["microservices/review/Microservice.ReviewService.Application.Contracts/Microservice.ReviewService.Application.Contracts.csproj", "microservices/review/Microservice.ReviewService.Application.Contracts/"]
COPY ["core/Microservice.Application/Microservice.Application.csproj", "core/Microservice.Application/"]
COPY ["infrastructure/Microservice.Infrastructure.AspNetCore/Microservice.Infrastructure.AspNetCore.csproj", "infrastructure/Microservice.Infrastructure.AspNetCore/"]
RUN dotnet restore "microservices/review/Microservice.ReviewService.Web/Microservice.ReviewService.Web.csproj"
COPY . .
WORKDIR "/src/microservices/review/Microservice.ReviewService.Web"
RUN dotnet build "Microservice.ReviewService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.ReviewService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.ReviewService.Web.dll"]