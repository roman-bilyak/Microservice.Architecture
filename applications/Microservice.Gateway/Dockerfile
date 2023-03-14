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
COPY ["microservices/identity/Microservice.IdentityService.Application.Abstractions/Microservice.IdentityService.Application.Abstractions.csproj", "microservices/identity/Microservice.IdentityService.Application.Abstractions/"]
COPY ["core/Microservice.Application.Abstractions/Microservice.Application.Abstractions.csproj", "core/Microservice.Application.Abstractions/"]
COPY ["microservices/identity/Microservice.IdentityService.Domain.Share/Microservice.IdentityService.Domain.Share.csproj", "microservices/identity/Microservice.IdentityService.Domain.Share/"]
COPY ["microservices/movie/Microservice.MovieService.Application.Abstractions/Microservice.MovieService.Application.Abstractions.csproj", "microservices/movie/Microservice.MovieService.Application.Abstractions/"]
COPY ["microservices/movie/Microservice.MovieService.Domain.Share/Microservice.MovieService.Domain.Share.csproj", "microservices/movie/Microservice.MovieService.Domain.Share/"]
COPY ["microservices/payment/Microservice.PaymentService.Application.Abstractions/Microservice.PaymentService.Application.Abstractions.csproj", "microservices/payment/Microservice.PaymentService.Application.Abstractions/"]
COPY ["microservices/payment/Microservice.PaymentService.Domain.Share/Microservice.PaymentService.Domain.Share.csproj", "microservices/payment/Microservice.PaymentService.Domain.Share/"]
COPY ["microservices/review/Microservice.ReviewService.Application.Abstractions/Microservice.ReviewService.Application.Abstractions.csproj", "microservices/review/Microservice.ReviewService.Application.Abstractions/"]
COPY ["microservices/review/Microservice.ReviewService.Domain.Share/Microservice.ReviewService.Domain.Share.csproj", "microservices/review/Microservice.ReviewService.Domain.Share/"]
COPY ["microservices/test/Microservice.TestService.Application.Abstractions/Microservice.TestService.Application.Abstrations.csproj", "microservices/test/Microservice.TestService.Application.Abstractions/"]
COPY ["microservices/test/Microservice.TestService.Domain.Share/Microservice.TestService.Domain.Share.csproj", "microservices/test/Microservice.TestService.Domain.Share/"]
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