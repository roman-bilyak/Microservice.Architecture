#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["gateways/Microservice.Gateway.Web/Microservice.Gateway.Web.csproj", "gateways/Microservice.Gateway.Web/"]
COPY ["microservices/test/Microservice.TestService.Application.Contracts/Microservice.TestService.Application.Contracts.csproj", "microservices/test/Microservice.TestService.Application.Contracts/"]
COPY ["microservices/test/Microservice.TestService.Domain.Share/Microservice.TestService.Domain.Share.csproj", "microservices/test/Microservice.TestService.Domain.Share/"]
COPY ["core/Microservice.Application/Microservice.Application.csproj", "core/Microservice.Application/"]
COPY ["infrastructure/Microservice.Infrastructure.AspNetCore/Microservice.Infrastructure.AspNetCore.csproj", "infrastructure/Microservice.Infrastructure.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/payment/Microservice.PaymentService.Application.Contracts/Microservice.PaymentService.Application.Contracts.csproj", "microservices/payment/Microservice.PaymentService.Application.Contracts/"]
COPY ["microservices/payment/Microservice.PaymentService.Domain.Share/Microservice.PaymentService.Domain.Share.csproj", "microservices/payment/Microservice.PaymentService.Domain.Share/"]
COPY ["microservices/movie/Microservice.MovieService.Application.Contracts/Microservice.MovieService.Application.Contracts.csproj", "microservices/movie/Microservice.MovieService.Application.Contracts/"]
COPY ["microservices/movie/Microservice.MovieService.Domain.Share/Microservice.MovieService.Domain.Share.csproj", "microservices/movie/Microservice.MovieService.Domain.Share/"]
COPY ["microservices/review/Microservice.ReviewService.Application.Contracts/Microservice.ReviewService.Application.Contracts.csproj", "microservices/review/Microservice.ReviewService.Application.Contracts/"]
COPY ["microservices/review/Microservice.ReviewService.Domain.Share/Microservice.ReviewService.Domain.Share.csproj", "microservices/review/Microservice.ReviewService.Domain.Share/"]
RUN dotnet restore "gateways/Microservice.Gateway.Web/Microservice.Gateway.Web.csproj"
COPY . .
WORKDIR "/src/gateways/Microservice.Gateway.Web"
RUN dotnet build "Microservice.Gateway.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Gateway.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Gateway.Web.dll"]