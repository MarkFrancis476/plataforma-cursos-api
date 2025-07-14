using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        // Un Curso TIENE MUCHOS Módulos
        builder.HasMany(c => c.Modules)
               // Un Módulo PERTENECE A UN Curso
               .WithOne(m => m.Course)
               // La clave foránea en la tabla Module es CourseId
               .HasForeignKey(m => m.CourseId)
               // Si se borra un curso, se borran sus módulos en cascada.
               .OnDelete(DeleteBehavior.Cascade);
    }
}