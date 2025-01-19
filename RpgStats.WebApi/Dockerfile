FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RpgStats.WebApi/RpgStats.WebApi.csproj", "RpgStats.WebApi/"]
COPY ["RpgStats.Repo/RpgStats.Repo.csproj", "RpgStats.Repo/"]
COPY ["RpgStats.Domain/RpgStats.Domain.csproj", "RpgStats.Domain/"]
COPY ["RpgStats.Services.Abstractions/RpgStats.Services.Abstractions.csproj", "RpgStats.Services.Abstractions/"]
COPY ["RpgStats.Dto/RpgStats.Dto.csproj", "RpgStats.Dto/"]
COPY ["RpgStats.Services/RpgStats.Services.csproj", "RpgStats.Services/"]
RUN dotnet restore "RpgStats.WebApi/RpgStats.WebApi.csproj"
COPY . .
WORKDIR "/src/RpgStats.WebApi"
RUN dotnet build "RpgStats.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
EXPOSE 7073
EXPOSE 5137
RUN dotnet publish "RpgStats.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RpgStats.WebApi.dll"]
