using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Persistence.DatabaseContext;

public class PlataformaCursosDbContext : DbContext
{
    // El constructor que recibe las opciones de configuración de la base de datos.
    public PlataformaCursosDbContext(DbContextOptions<PlataformaCursosDbContext> options) : base(options)
    {
    }

    // DbSet<T>: Representa una tabla en la base de datos para la entidad T.
    public DbSet<Domain.Entities.Course> Courses { get; set; }
    public DbSet<Domain.Entities.Instructor> Instructors { get; set; }
    public DbSet<Domain.Entities.Module> Modules { get; set; } // La línea clave a cambiar
    public DbSet<Domain.Entities.Lesson> Lessons { get; set; }

    // Este método se llama cuando EF Core está creando el modelo de la base de datos.
    // Aquí es donde configuramos las relaciones, los tipos de datos, etc.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Esto busca todas las configuraciones de entidades (que crearemos a continuación)
        // en el mismo ensamblado (proyecto) donde está esta clase y las aplica.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}