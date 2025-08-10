# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
EXPOSE 5026


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["InfotecsWebAPI.Web/InfotecsWebAPI.Web.csproj", "InfotecsWebAPI.Web/"]
COPY ["InfotecsWebAPI.Application/InfotecsWebAPI.Application.csproj", "InfotecsWebAPI.Application/"]
COPY ["InfotecsWebAPI.Domain/InfotecsWebAPI.Domain.csproj", "InfotecsWebAPI.Domain/"]
COPY ["InfotecsWebAPI.Persistence/InfotecsWebAPI.Persistence.csproj", "InfotecsWebAPI.Persistence/"]
COPY ["InfotecsWebAPI.CsvParser/InfotecsWebAPI.SepCsvParser.csproj", "InfotecsWebAPI.CsvParser/"]
RUN dotnet restore -v normal "./InfotecsWebAPI.Web/InfotecsWebAPI.Web.csproj"
COPY . .
WORKDIR "/src/InfotecsWebAPI.Web"
RUN dotnet build "./InfotecsWebAPI.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InfotecsWebAPI.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InfotecsWebAPI.Web.dll"]
