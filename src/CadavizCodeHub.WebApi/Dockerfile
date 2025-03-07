# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# Cria um usuário e grupo não root
RUN groupadd -r appgroup && useradd -r -g appgroup -m appuser

# Define o usuário para rodar o container
USER appuser

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Create a new user
RUN groupadd -r appgroup && useradd -r -g appgroup -m appuser

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["NuGet.config", "."]
COPY ["src/CadavizCodeHub.WebApi/CadavizCodeHub.WebApi.csproj", "src/CadavizCodeHub.WebApi/"]
COPY ["src/CadavizCodeHub.Application/CadavizCodeHub.Application.csproj", "src/CadavizCodeHub.Application/"]
COPY ["src/CadavizCodeHub.Domain/CadavizCodeHub.Domain.csproj", "src/CadavizCodeHub.Domain/"]
COPY ["src/CadavizCodeHub.Framework/CadavizCodeHub.Framework.csproj", "src/CadavizCodeHub.Framework/"]
COPY ["src/CadavizCodeHub.Infrastructure/CadavizCodeHub.Persistence.csproj", "src/CadavizCodeHub.Infrastructure/"]
RUN dotnet restore "./src/CadavizCodeHub.WebApi/CadavizCodeHub.WebApi.csproj"

COPY ["src/CadavizCodeHub.WebApi/", "src/CadavizCodeHub.WebApi/"]
COPY ["src/CadavizCodeHub.Application/", "src/CadavizCodeHub.Application/"]
COPY ["src/CadavizCodeHub.Domain/", "src/CadavizCodeHub.Domain/"]
COPY ["src/CadavizCodeHub.Framework/", "src/CadavizCodeHub.Framework/"]
COPY ["src/CadavizCodeHub.Infrastructure/", "src/CadavizCodeHub.Infrastructure/"]
WORKDIR "/src/src/CadavizCodeHub.WebApi"
RUN dotnet build "./CadavizCodeHub.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CadavizCodeHub.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
USER appuser
ENTRYPOINT ["dotnet", "CadavizCodeHub.WebApi.dll"]