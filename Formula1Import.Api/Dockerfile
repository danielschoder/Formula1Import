# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

COPY ./Formula1Import.Api/appsettings.json /app/appsettings.json

# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Formula1Import.Api/Formula1Import.Api.csproj", "Formula1Import.Api/"]
COPY ["Formula1Import.Infrastructure/Formula1Import.Infrastructure.csproj", "Formula1Import.Infrastructure/"]
COPY ["Formula1Import.Domain/Formula1Import.Domain.csproj", "Formula1Import.Domain/"]
COPY ["Formula1Import.Application/Formula1Import.Application.csproj", "Formula1Import.Application/"]
COPY ["Formula1Import.Contracts/Formula1Import.Contracts.csproj", "Formula1Import.Contracts/"]
RUN dotnet restore "./Formula1Import.Api/Formula1Import.Api.csproj"
COPY . .
WORKDIR "/src/Formula1Import.Api"
RUN dotnet build "./Formula1Import.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Formula1Import.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Formula1Import.Api.dll"]