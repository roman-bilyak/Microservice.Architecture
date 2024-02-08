#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["microservices/payment/Microservice.Api.PaymentService/Microservice.Api.PaymentService.csproj", "microservices/payment/Microservice.Api.PaymentService/"]
COPY ["core/Microservice.Api/Microservice.Api.csproj", "core/Microservice.Api/"]
COPY ["core/Microservice.Api.AspNetCore.Authentication/Microservice.Api.AspNetCore.Authentication.csproj", "core/Microservice.Api.AspNetCore.Authentication/"]
COPY ["core/Microservice.Api.AspNetCore/Microservice.Api.AspNetCore.csproj", "core/Microservice.Api.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["core/Microservice.Api.AspNetCore.Authorization/Microservice.Api.AspNetCore.Authorization.csproj", "core/Microservice.Api.AspNetCore.Authorization/"]
COPY ["core/Microservice.Api.AspNetCore.Swagger/Microservice.Api.AspNetCore.Swagger.csproj", "core/Microservice.Api.AspNetCore.Swagger/"]
COPY ["microservices/payment/Microservice.Application.PaymentService/Microservice.Application.PaymentService.csproj", "microservices/payment/Microservice.Application.PaymentService/"]
COPY ["core/Microservice.Application.CQRS/Microservice.Application.CQRS.csproj", "core/Microservice.Application.CQRS/"]
COPY ["core/Microservice.Application/Microservice.Application.csproj", "core/Microservice.Application/"]
COPY ["core/Microservice.Application.Abstractions/Microservice.Application.Abstractions.csproj", "core/Microservice.Application.Abstractions/"]
COPY ["microservices/payment/Microservice.Application.PaymentService.Abstractions/Microservice.Application.PaymentService.Abstractions.csproj", "microservices/payment/Microservice.Application.PaymentService.Abstractions/"]
COPY ["microservices/payment/Microservice.Domain.PaymentService.Shared/Microservice.Domain.PaymentService.Shared.csproj", "microservices/payment/Microservice.Domain.PaymentService.Shared/"]
COPY ["microservices/payment/Microservice.Domain.PaymentService/Microservice.Domain.PaymentService.csproj", "microservices/payment/Microservice.Domain.PaymentService/"]
COPY ["core/Microservice.Domain/Microservice.Domain.csproj", "core/Microservice.Domain/"]
COPY ["core/Microservice.Infrastructure.Abstractions/Microservice.Infrastructure.Abstractions.csproj", "core/Microservice.Infrastructure.Abstractions/"]
COPY ["microservices/payment/Microservice.Infrastructure.PaymentService/Microservice.Infrastructure.PaymentService.csproj", "microservices/payment/Microservice.Infrastructure.PaymentService/"]
COPY ["core/Microservice.Infrastructure.EntityFrameworkCore/Microservice.Infrastructure.EntityFrameworkCore.csproj", "core/Microservice.Infrastructure.EntityFrameworkCore/"]
RUN dotnet restore "microservices/payment/Microservice.Api.PaymentService/Microservice.Api.PaymentService.csproj"
COPY . .
WORKDIR "/src/microservices/payment/Microservice.Api.PaymentService"
RUN dotnet build "Microservice.Api.PaymentService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.Api.PaymentService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.Api.PaymentService.dll"]