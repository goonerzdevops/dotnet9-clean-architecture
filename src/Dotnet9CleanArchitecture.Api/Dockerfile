FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/Dotnet9CleanArchitecture.Api/Dotnet9CleanArchitecture.Api.csproj", "src/Dotnet9CleanArchitecture.Api/"]
COPY ["src/Dotnet9CleanArchitecture.Application/Dotnet9CleanArchitecture.Application.csproj", "src/Dotnet9CleanArchitecture.Application/"]
COPY ["src/Dotnet9CleanArchitecture.Domain/Dotnet9CleanArchitecture.Domain.csproj", "src/Dotnet9CleanArchitecture.Domain/"]
COPY ["src/Dotnet9CleanArchitecture.Infrastructure/Dotnet9CleanArchitecture.Infrastructure.csproj", "src/Dotnet9CleanArchitecture.Infrastructure/"]
RUN dotnet restore "src/Dotnet9CleanArchitecture.Api/Dotnet9CleanArchitecture.Api.csproj"

COPY . .
WORKDIR "/src/src/Dotnet9CleanArchitecture.Api"
RUN dotnet build -c $BUILD_CONFIGURATION -o /app/build --no-restore

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish -c $BUILD_CONFIGURATION -o /app/publish --no-restore /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet9CleanArchitecture.Api.dll"]
