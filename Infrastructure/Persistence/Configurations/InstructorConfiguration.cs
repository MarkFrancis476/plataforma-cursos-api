using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class InstructorConfiguration : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        // Un Instructor TIENE MUCHOS Cursos
        builder.HasMany(i => i.Courses)
               // Un Curso PERTENECE A UN Instructor
               .WithOne(c => c.Instructor)
               // La clave foránea en la tabla Course es InstructorId
               .HasForeignKey(c => c.InstructorId)
               // ¡IMPORTANTE! Si se intenta borrar un instructor, no hagas nada en la BD.
               // Nuestra lógica de negocio en el handler se encargará de prevenirlo.
               // Si lo pusiéramos en Cascade, la BD borraría los cursos y violaría nuestra regla.
               .OnDelete(DeleteBehavior.Restrict); 
    }
}