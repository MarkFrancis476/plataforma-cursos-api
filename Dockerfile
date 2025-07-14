# --- Etapa 1: Build (Construcción) ---
# Usamos la imagen oficial del SDK de .NET 8 para compilar el proyecto.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiamos todos los archivos .csproj y el .sln y restauramos los paquetes.
# Hacemos esto en un paso separado para aprovechar el caché de Docker.
COPY ["Api/Api.csproj", "Api/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["PlataformaCursos.sln", "."]
RUN dotnet restore "PlataformaCursos.sln"

# Copiamos el resto del código fuente.
COPY . .
WORKDIR "/src/Api"
# Compilamos el proyecto Api en modo Release y lo dejamos en la carpeta /app/build.
RUN dotnet build "Api.csproj" -c Release -o /app/build


# --- Etapa 2: Publish (Publicación) ---
# Continuamos desde la etapa de build.
FROM build AS publish
# Publicamos la aplicación, optimizándola para producción.
RUN dotnet publish "Api.csproj" -c Release -o /app/publish /p:UseAppHost=false


# --- Etapa 3: Final (Ejecución) ---
# Usamos una imagen mucho más ligera que solo contiene lo necesario para ejecutar la API.
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
# Exponemos el puerto 8080 (Railway usará este puerto internamente).
EXPOSE 8080
# Copiamos solo los archivos publicados de la etapa anterior.
COPY --from=publish /app/publish .
# El comando final para iniciar la API cuando el contenedor se ejecute.
ENTRYPOINT ["dotnet", "Api.dll"]