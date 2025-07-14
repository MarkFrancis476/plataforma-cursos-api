# --- Etapa 1: Build (Construcción) ---
# Usamos la imagen oficial del SDK de .NET 8.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos TODOS los archivos del proyecto al contenedor.
COPY . .

# Restauramos las dependencias de toda la solución.
RUN dotnet restore "PlataformaCursos.sln"

# Compilamos y publicamos el proyecto de la API en una sola instrucción.
# Lo compilamos en modo Release y lo dejamos en la carpeta /app/publish.
RUN dotnet publish "Api/Api.csproj" -c Release -o /app/publish


# --- Etapa 2: Final (Ejecución) ---
# Usamos una imagen mucho más ligera que solo contiene lo necesario para ejecutar la API.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
# Exponemos el puerto 8080 (Railway lo usará internamente).
EXPOSE 8080
# Copiamos solo los archivos publicados de la etapa anterior.
COPY --from=build /app/publish .
# El comando final para iniciar la API.
ENTRYPOINT ["dotnet", "Api.dll"]