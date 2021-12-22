#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microservices/payment/Microservice.PaymentService.Web/Microservice.PaymentService.Web.csproj", "microservices/payment/Microservice.PaymentService.Web/"]
COPY ["microservices/payment/Microservice.PaymentService.Application/Microservice.PaymentService.Application.csproj", "microservices/payment/Microservice.PaymentService.Application/"]
COPY ["microservices/payment/Microservice.PaymentService.Domain/Microservice.PaymentService.Domain.csproj", "microservices/payment/Microservice.PaymentService.Domain/"]
COPY ["microservices/payment/Microservice.PaymentService.Domain.Share/Microservice.PaymentService.Domain.Share.csproj", "microservices/payment/Microservice.PaymentService.Domain.Share/"]
COPY ["microservices/payment/Microservice.PaymentService.Application.Contracts/Microservice.PaymentService.Application.Contracts.csproj", "microservices/payment/Microservice.PaymentService.Application.Contracts/"]
COPY ["microservices/payment/Microservice.PaymentService.Infrastructure/Microservice.PaymentService.Infrastructure.csproj", "microservices/payment/Microservice.PaymentService.Infrastructure/"]
RUN dotnet restore "microservices/payment/Microservice.PaymentService.Web/Microservice.PaymentService.Web.csproj"
COPY . .
WORKDIR "/src/microservices/payment/Microservice.PaymentService.Web"
RUN dotnet build "Microservice.PaymentService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.PaymentService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.PaymentService.Web.dll"]