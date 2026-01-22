FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copiar apenas os csproj necessários
COPY Worker/Worker.csproj Worker/
COPY Infra/Infra.csproj Infra/
COPY Repository/Repository.csproj Repository/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/


RUN dotnet restore Worker/Worker.csproj

# Copiar o restante
COPY . .

RUN dotnet publish Worker/Worker.csproj -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .

ENTRYPOINT ["dotnet", "Worker.dll"]
