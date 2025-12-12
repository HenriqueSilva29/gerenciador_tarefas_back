FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar a solução
COPY API.sln ./

# Copiar os csproj primeiro (cache)
COPY API/API.csproj API/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Repository/Repository.csproj Repository/
COPY Infra/Infra.csproj Infra/
COPY Worker/Worker.csproj Worker/

RUN dotnet restore

# Copiar TODO O RESTO do código
COPY . .

RUN dotnet publish Worker/Worker.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Worker.dll"]
