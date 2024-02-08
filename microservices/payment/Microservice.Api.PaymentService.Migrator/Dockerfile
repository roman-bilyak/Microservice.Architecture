#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["microservices/payment/Microservice.Api.PaymentService.Migrator/Microservice.Api.PaymentService.Migrator.csproj", "microservices/payment/Microservice.Api.PaymentService.Migrator/"]
COPY ["microservices/payment/Microservice.Infrastructure.PaymentService/Microservice.Infrastructure.PaymentService.csproj", "microservices/payment/Microservice.Infrastructure.PaymentService/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/payment/Microservice.Domain.PaymentService/Microservice.Domain.PaymentService.csproj", "microservices/payment/Microservice.Domain.PaymentService/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["microservices/payment/Microservice.Domain.PaymentService.Shared/Microservice.Domain.PaymentService.Shared.csproj", "microservices/payment/Microservice.Domain.PaymentService.Shared/"]
RUN dotnet restore "microservices/payment/Microservice.Api.PaymentService.Migrator/Microservice.Api.PaymentService.Migrator.csproj"
COPY . .
WORKDIR "/src/microservices/payment/Microservice.Api.PaymentService.Migrator"
RUN dotnet build "Microservice.Api.PaymentService.Migrator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Api.PaymentService.Migrator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Api.PaymentService.Migrator.dll"]