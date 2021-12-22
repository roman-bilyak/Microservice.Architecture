#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["microservices/identity/Microservice.IdentityService.Web/Microservice.IdentityService.Web.csproj", "microservices/identity/Microservice.IdentityService.Web/"]
RUN dotnet restore "microservices/identity/Microservice.IdentityService.Web/Microservice.IdentityService.Web.csproj"
COPY . .
WORKDIR "/src/microservices/identity/Microservice.IdentityService.Web"
RUN dotnet build "Microservice.IdentityService.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Microservice.IdentityService.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Microservice.IdentityService.Web.dll"]