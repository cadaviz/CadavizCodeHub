# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/CadavizCodeHub.Orders.WebApi/CadavizCodeHub.Orders.WebApi.csproj", "src/CadavizCodeHub.Orders.WebApi/"]
COPY ["src/CadavizCodeHub.Core.Logging/CadavizCodeHub.Core.Logging.csproj", "src/CadavizCodeHub.Core.Logging/"]
COPY ["src/CadavizCodeHub.Core.Shared/CadavizCodeHub.Core.Shared.csproj", "src/CadavizCodeHub.Core.Shared/"]
COPY ["src/CadavizCodeHub.Core.WebApi/CadavizCodeHub.Core.WebApi.csproj", "src/CadavizCodeHub.Core.WebApi/"]
COPY ["src/CadavizCodeHub.Orders.Application/CadavizCodeHub.Orders.Application.csproj", "src/CadavizCodeHub.Orders.Application/"]
COPY ["src/CadavizCodeHub.Core.Application/CadavizCodeHub.Core.Application.csproj", "src/CadavizCodeHub.Core.Application/"]
COPY ["src/CadavizCodeHub.Orders.Domain/CadavizCodeHub.Orders.Domain.csproj", "src/CadavizCodeHub.Orders.Domain/"]
COPY ["src/CadavizCodeHub.Core.Domain/CadavizCodeHub.Core.Domain.csproj", "src/CadavizCodeHub.Core.Domain/"]
COPY ["src/CadavizCodeHub.Orders.Persistence/CadavizCodeHub.Orders.Persistence.csproj", "src/CadavizCodeHub.Orders.Persistence/"]
COPY ["src/CadavizCodeHub.Core.Persistence.MongoDB/CadavizCodeHub.Core.Persistence.MongoDB.csproj", "src/CadavizCodeHub.Core.Persistence.MongoDB/"]
COPY ["src/CadavizCodeHub.Core.Persistence/CadavizCodeHub.Core.Persistence.csproj", "src/CadavizCodeHub.Core.Persistence/"]
RUN dotnet restore "./src/CadavizCodeHub.Orders.WebApi/CadavizCodeHub.Orders.WebApi.csproj"
COPY . .
WORKDIR "/src/src/CadavizCodeHub.Orders.WebApi"
RUN dotnet build "./CadavizCodeHub.Orders.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CadavizCodeHub.Orders.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Criar diretório para o certificado
RUN mkdir -p /https

# Gerar o certificado HTTPS
RUN dotnet dev-certs https --clean && \
    dotnet dev-certs https --trust && \
    dotnet dev-certs https -ep /https/CadavizCodeHub.Orders.WebApi.pfx -p password && \
    chmod 644 /https/CadavizCodeHub.Orders.WebApi.pfx

ENTRYPOINT ["dotnet", "CadavizCodeHub.Orders.WebApi.dll"]