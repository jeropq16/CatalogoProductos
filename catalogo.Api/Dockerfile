FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["catalogo.Api/catalogo.Api.csproj", "catalogo.Api/"]
COPY ["catalogo.Application/catalogo.Application.csproj", "catalogo.Application/"]
COPY ["catalogo.Domain/catalogo.Domain.csproj", "catalogo.Domain/"]
COPY ["catalogo.Infrastructure/catalogo.Infrastructure.csproj", "catalogo.Infrastructure/"]
RUN dotnet restore "catalogo.Api/catalogo.Api.csproj"
COPY . .
WORKDIR "/src/catalogo.Api"
RUN dotnet build "./catalogo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./catalogo.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "catalogo.Api.dll"]
