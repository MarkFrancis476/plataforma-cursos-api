# --- Etapa 1: Build (Construcción) ---
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos TODOS los archivos del proyecto al contenedor.
COPY . .

# Nos enfocamos directamente en el proyecto de la API para la publicación.
# El comando 'publish' se encargará de restaurar, construir y empaquetar
# el proyecto Api y todas sus dependencias (Application, Domain, Infrastructure).
WORKDIR /src/Api
RUN dotnet publish -c Release -o /app/publish


# --- Etapa 2: Final (Ejecución) ---
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
# El punto de entrada sigue siendo la DLL del proyecto Api.
ENTRYPOINT ["dotnet", "Api.dll"]