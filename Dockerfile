# Etapa 1: Restaurar dependencias
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS restore
WORKDIR /src
COPY . .
RUN dotnet restore "PlataformaCursos.sln"

# Etapa 2: Construir el proyecto
FROM restore AS build
WORKDIR /src
RUN dotnet build "PlataformaCursos.sln" -c Release --no-restore

# Etapa 3: Publicar el proyecto de la API
FROM build AS publish
WORKDIR /src/Api
RUN dotnet publish "Api.csproj" -c Release -o /app/publish --no-build

# Etapa 4: Crear la imagen final de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.dll"]