# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar csproj primeiro para aproveitar cache
COPY API.sln ./

COPY API/API.csproj API/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Repository/Repository.csproj Repository/
COPY Infra/Infra.csproj Infra/
COPY Worker/Worker.csproj Worker/

RUN dotnet restore

# Copiar o restante
COPY . .

RUN dotnet publish API/API.csproj -c Release -o /app

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "API.dll"]
