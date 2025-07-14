# Tarea 2: Plataforma de Cursos API

Este proyecto es una API REST para una plataforma de cursos online, desarrollada como parte de la Tarea 2. La solución está construida siguiendo los principios de la Programación Orientada a Objetos y una Arquitectura Limpia.

## Autores
*   Isaias Aaron Couoh Cauich
*   Isaac Emmanuel Ciau Aguilar

## Tecnologías Utilizadas
*   **.NET 8**: Framework de desarrollo.
*   **ASP.NET Core**: Para la construcción de la API.
*   **Entity Framework Core**: ORM para la interacción con la base de datos.
*   **PostgreSQL**: Motor de base de datos.
*   **MediatR**: Para implementar los patrones CQRS y Mediator en la capa de aplicación.
*   **Railway**: Para el despliegue de la API y la base de datos.
*   **Swagger (Swashbuckle)**: Para la documentación interactiva de la API.

## Arquitectura
El proyecto sigue una Arquitectura Limpia dividida en cuatro capas:
- **Domain**: Contiene las entidades de negocio y las reglas críticas.
- **Application**: Orquesta los casos de uso y define las interfaces.
- **Infrastructure**: Implementa la persistencia con EF Core y otros servicios externos.
- **Api**: Expone los endpoints REST al exterior.

## URL de la API Desplegada
La API está desplegada y se puede acceder a través de la siguiente URL:
**https://plataforma-cursos-api-production.up.railway.app/swagger/index.html**

## Cómo Ejecutar el Proyecto Localmente
1.  Clonar el repositorio: `git clone [URL_DEL_REPO]`
2.  Navegar a la carpeta del proyecto.
3.  Asegurarse de tener el SDK de .NET 8 instalado.
4.  Configurar la cadena de conexión en `Api/appsettings.json` si se desea usar una base de datos local.
5.  Ejecutar el comando: `dotnet run --project Api`
6.  Acceder a Swagger en `http://localhost:[puerto]/swagger`.
