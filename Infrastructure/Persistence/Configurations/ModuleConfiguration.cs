using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class ModuleConfiguration : IEntityTypeConfiguration<Module>
{
    public void Configure(EntityTypeBuilder<Module> builder)
    {
        // Un Módulo TIENE MUCHAS Lecciones
        builder.HasMany(m => m.Lessons)
               // Una Lección PERTENECE A UN Módulo
               .WithOne(l => l.Module)
               // La clave foránea en la tabla Lesson es ModuleId
               .HasForeignKey(l => l.ModuleId)
               // Si se borra un módulo, se borran sus lecciones en cascada.
               .OnDelete(DeleteBehavior.Cascade);
    }
}