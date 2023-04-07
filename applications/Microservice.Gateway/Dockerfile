#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["applications/Microservice.Gateway/Microservice.Gateway.csproj", "applications/Microservice.Gateway/"]
COPY ["core/Microservice.Api/Microservice.Api.csproj", "core/Microservice.Api/"]
COPY ["core/Microservice.Api.AspNetCore.Authentication/Microservice.Api.AspNetCore.Authentication.csproj", "core/Microservice.Api.AspNetCore.Authentication/"]
COPY ["core/Microservice.Api.AspNetCore/Microservice.Api.AspNetCore.csproj", "core/Microservice.Api.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["core/Microservice.Api.AspNetCore.Authorization/Microservice.Api.AspNetCore.Authorization.csproj", "core/Microservice.Api.AspNetCore.Authorization/"]
COPY ["core/Microservice.Api.AspNetCore.Swagger/Microservice.Api.AspNetCore.Swagger.csproj", "core/Microservice.Api.AspNetCore.Swagger/"]
COPY ["microservices/identity/Microservice.Application.IdentityService.Abstractions/Microservice.Application.IdentityService.Abstractions.csproj", "microservices/identity/Microservice.Application.IdentityService.Abstractions/"]
COPY ["core/Microservice.Application.Abstractions/Microservice.Application.Abstractions.csproj", "core/Microservice.Application.Abstractions/"]
COPY ["microservices/identity/Microservice.Domain.IdentityService.Shared/Microservice.Domain.IdentityService.Shared.csproj", "microservices/identity/Microservice.Domain.IdentityService.Shared/"]
COPY ["microservices/movie/Microservice.Application.MovieService.Abstractions/Microservice.Application.MovieService.Abstractions.csproj", "microservices/movie/Microservice.Application.MovieService.Abstractions/"]
COPY ["microservices/movie/Microservice.Domain.MovieService.Shared/Microservice.Domain.MovieService.Shared.csproj", "microservices/movie/Microservice.Domain.MovieService.Shared/"]
COPY ["microservices/payment/Microservice.Application.PaymentService.Abstractions/Microservice.Application.PaymentService.Abstractions.csproj", "microservices/payment/Microservice.Application.PaymentService.Abstractions/"]
COPY ["microservices/payment/Microservice.Domain.PaymentService.Shared/Microservice.Domain.PaymentService.Shared.csproj", "microservices/payment/Microservice.Domain.PaymentService.Shared/"]
COPY ["microservices/review/Microservice.Application.ReviewService.Abstractions/Microservice.Application.ReviewService.Abstractions.csproj", "microservices/review/Microservice.Application.ReviewService.Abstractions/"]
COPY ["microservices/review/Microservice.Domain.ReviewService.Shared/Microservice.Domain.ReviewService.Shared.csproj", "microservices/review/Microservice.Domain.ReviewService.Shared/"]
COPY ["microservices/test/Microservice.Application.TestService.Abstractions/Microservice.Application.TestService.Abstrations.csproj", "microservices/test/Microservice.Application.TestService.Abstractions/"]
COPY ["microservices/test/Microservice.Domain.TestService.Shared/Microservice.Domain.TestService.Shared.csproj", "microservices/test/Microservice.Domain.TestService.Shared/"]
RUN dotnet restore "applications/Microservice.Gateway/Microservice.Gateway.csproj"
COPY . .
WORKDIR "/src/applications/Microservice.Gateway"
RUN dotnet build "Microservice.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Gateway.dll"]