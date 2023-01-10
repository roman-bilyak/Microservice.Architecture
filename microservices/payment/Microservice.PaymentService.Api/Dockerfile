#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["microservices/payment/Microservice.PaymentService.Api/Microservice.PaymentService.Api.csproj", "microservices/payment/Microservice.PaymentService.Api/"]
COPY ["core/Microservice.Api/Microservice.Api.csproj", "core/Microservice.Api/"]
COPY ["core/Microservice.AspNetCore.Authentication/Microservice.AspNetCore.Authentication.csproj", "core/Microservice.AspNetCore.Authentication/"]
COPY ["core/Microservice.AspNetCore/Microservice.AspNetCore.csproj", "core/Microservice.AspNetCore/"]
COPY ["core/Microservice.Core/Microservice.Core.csproj", "core/Microservice.Core/"]
COPY ["core/Microservice.AspNetCore.Authorization/Microservice.AspNetCore.Authorization.csproj", "core/Microservice.AspNetCore.Authorization/"]
COPY ["core/Microservice.AspNetCore.Swagger/Microservice.AspNetCore.Swagger.csproj", "core/Microservice.AspNetCore.Swagger/"]
COPY ["microservices/payment/Microservice.PaymentService.Application/Microservice.PaymentService.Application.csproj", "microservices/payment/Microservice.PaymentService.Application/"]
COPY ["microservices/payment/Microservice.PaymentService.Application.Abstractions/Microservice.PaymentService.Application.Abstractions.csproj", "microservices/payment/Microservice.PaymentService.Application.Abstractions/"]
COPY ["core/Microservice.Application.Abstractions/Microservice.Application.Abstractions.csproj", "core/Microservice.Application.Abstractions/"]
COPY ["microservices/payment/Microservice.PaymentService.Domain.Share/Microservice.PaymentService.Domain.Share.csproj", "microservices/payment/Microservice.PaymentService.Domain.Share/"]
COPY ["microservices/payment/Microservice.PaymentService.Domain/Microservice.PaymentService.Domain.csproj", "microservices/payment/Microservice.PaymentService.Domain/"]
COPY ["core/Microservice.Database.Abstractions/Microservice.Database.Abstractions.csproj", "core/Microservice.Database.Abstractions/"]
COPY ["microservices/payment/Microservice.PaymentService.Infrastructure/Microservice.PaymentService.Infrastructure.csproj", "microservices/payment/Microservice.PaymentService.Infrastructure/"]
COPY ["core/Microservice.Database.EntityFrameworkCore/Microservice.Database.EntityFrameworkCore.csproj", "core/Microservice.Database.EntityFrameworkCore/"]
RUN dotnet restore "microservices/payment/Microservice.PaymentService.Api/Microservice.PaymentService.Api.csproj"
COPY . .
WORKDIR "/src/microservices/payment/Microservice.PaymentService.Api"
RUN dotnet build "Microservice.PaymentService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.PaymentService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.PaymentService.Api.dll"]