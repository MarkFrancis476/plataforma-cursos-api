using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure.Persistence.DatabaseContext;

// Esta clase le enseña a la herramienta 'dotnet ef' cómo crear nuestro DbContext.
public class PlataformaCursosDbContextFactory : IDesignTimeDbContextFactory<PlataformaCursosDbContext>
{
    public PlataformaCursosDbContext CreateDbContext(string[] args)
    {
        // 1. Crear un objeto de configuración para poder leer appsettings.json
        IConfigurationRoot configuration = new ConfigurationBuilder()
            // Le decimos que la base de la ruta es el directorio del proyecto Api
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Api"))
            // Le decimos que lea el archivo appsettings.json
            .AddJsonFile("appsettings.json")
            .Build();

        // 2. Crear un constructor de opciones para nuestro DbContext
        var optionsBuilder = new DbContextOptionsBuilder<PlataformaCursosDbContext>();

        // 3. Obtener la cadena de conexión desde el archivo de configuración
        var connectionString = configuration.GetConnectionString("PlataformaCursosConnection");

        // 4. Configurar el DbContext para que use PostgreSQL con esa cadena de conexión
        optionsBuilder.UseNpgsql(connectionString);

        // 5. Devolver una nueva instancia del DbContext con las opciones configuradas
        return new PlataformaCursosDbContext(optionsBuilder.Options);
    }
}