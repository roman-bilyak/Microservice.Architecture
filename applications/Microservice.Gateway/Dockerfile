#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["applications/Microservice.Gateway/Microservice.Gateway.csproj", "applications/Microservice.Gateway/"]
COPY ["core/Microservice.Api/Microservice.Api.csproj", "core/Microservice.Api/"]
COPY ["core/Microservice.AspNetCore.Authentication/Microservice.AspNetCore.Authentication.csproj", "core/Microservice.AspNetCore.Authentication/"]
COPY ["core/Microservice.AspNetCore/Microservice.AspNetCore.csproj", "core/Microservice.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["core/Microservice.AspNetCore.Authorization/Microservice.AspNetCore.Authorization.csproj", "core/Microservice.AspNetCore.Authorization/"]
COPY ["core/Microservice.AspNetCore.Swagger/Microservice.AspNetCore.Swagger.csproj", "core/Microservice.AspNetCore.Swagger/"]
COPY ["microservices/movie/Microservice.MovieService.Application.Contracts/Microservice.MovieService.Application.Contracts.csproj", "microservices/movie/Microservice.MovieService.Application.Contracts/"]
COPY ["core/Microservice.Application/Microservice.Application.csproj", "core/Microservice.Application/"]
COPY ["microservices/movie/Microservice.MovieService.Domain.Share/Microservice.MovieService.Domain.Share.csproj", "microservices/movie/Microservice.MovieService.Domain.Share/"]
COPY ["microservices/payment/Microservice.PaymentService.Application.Contracts/Microservice.PaymentService.Application.Contracts.csproj", "microservices/payment/Microservice.PaymentService.Application.Contracts/"]
COPY ["microservices/payment/Microservice.PaymentService.Domain.Share/Microservice.PaymentService.Domain.Share.csproj", "microservices/payment/Microservice.PaymentService.Domain.Share/"]
COPY ["microservices/review/Microservice.ReviewService.Application.Contracts/Microservice.ReviewService.Application.Contracts.csproj", "microservices/review/Microservice.ReviewService.Application.Contracts/"]
COPY ["microservices/review/Microservice.ReviewService.Domain.Share/Microservice.ReviewService.Domain.Share.csproj", "microservices/review/Microservice.ReviewService.Domain.Share/"]
COPY ["microservices/test/Microservice.TestService.Application.Contracts/Microservice.TestService.Application.Contracts.csproj", "microservices/test/Microservice.TestService.Application.Contracts/"]
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