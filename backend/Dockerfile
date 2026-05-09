# ── Build stage ────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution + all project files first (layer-cache friendly)
COPY CST.LePoint.sln .
COPY src/ src/

# Restore NuGet packages
RUN dotnet restore CST.LePoint.sln

# Build CSS (tailwind via npm) before the .NET build
WORKDIR /src/src/CollectManagement.ms/CollectManagement.WebAPI
RUN apt-get update -y && apt-get install -y nodejs npm && npm install && npm run build:css || true
WORKDIR /src

# Publish
RUN dotnet publish src/CollectManagement.ms/CollectManagement.WebAPI/CollectManagement.WebAPI.csproj \
    -c Release -o /app/publish --no-restore

# ── Runtime stage ───────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "CollectManagement.WebAPI.dll"]
