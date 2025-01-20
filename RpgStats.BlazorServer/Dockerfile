FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RpgStats.BlazorServer/RpgStats.BlazorServer.csproj", "RpgStats.BlazorServer/"]
COPY ["RpgStats.BizLogic/RpgStats.BizLogic.csproj", "RpgStats.BizLogic/"]
COPY ["RpgStats.Dto/RpgStats.Dto.csproj", "RpgStats.Dto/"]
COPY ["RpgStats.Domain/RpgStats.Domain.csproj", "RpgStats.Domain/"]
RUN dotnet restore "RpgStats.BlazorServer/RpgStats.BlazorServer.csproj"
COPY . .
WORKDIR "/src/RpgStats.BlazorServer"
RUN dotnet build "RpgStats.BlazorServer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "RpgStats.BlazorServer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RpgStats.BlazorServer.dll"]
