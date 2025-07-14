using Application.Contracts.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore; // ¡Muy importante! Necesario para usar métodos como ToListAsync, FirstOrDefaultAsync, etc.
using System;
using System.Collections.Generic;
using System.Linq; // Necesario para usar Include
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

// Esta clase hereda de una clase base (que no hemos creado pero es buena práctica) y
// más importante, implementa la interfaz IInstructorRepository.
// Al implementar la interfaz, esta clase "promete" que tendrá todos los métodos definidos en ella.
public class InstructorRepository : IInstructorRepository
{
    // Un campo privado para guardar la referencia a nuestro DbContext.
    // Es 'readonly' porque solo se asignará una vez, en el constructor.
    private readonly PlataformaCursosDbContext _context;

    // El constructor. Recibe el DbContext a través de inyección de dependencias.
    public InstructorRepository(PlataformaCursosDbContext context)
    {
        _context = context;
    }

    // --- IMPLEMENTACIÓN DE LOS MÉTODOS DE LA INTERFAZ ---

    public async Task AddAsync(Instructor instructor, CancellationToken cancellationToken)
    {
        // _context.Instructors es nuestro DbSet<Instructor> (la tabla de instructores).
        // AddAsync es un método de EF Core que marca la nueva entidad para ser insertada en la BD.
        await _context.Instructors.AddAsync(instructor, cancellationToken);
    }

    public void Delete(Instructor instructor)
    {
        // El método Remove de EF Core marca la entidad para ser eliminada de la BD.
        // No es asíncrono porque solo cambia el estado del objeto en memoria.
        _context.Instructors.Remove(instructor);
    }

    public async Task<IReadOnlyList<Instructor>> GetAllAsync(CancellationToken cancellationToken)
    {
        // ToListAsync() es un método de EF Core que ejecuta la consulta SQL
        // correspondiente (SELECT * FROM "Instructors") y devuelve una lista de resultados.
        return await _context.Instructors.ToListAsync(cancellationToken);
    }

    public async Task<Instructor?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // FirstOrDefaultAsync es un método de EF Core que busca el primer elemento
        // que cumpla la condición (i => i.Id == id).
        // Si no encuentra ninguno, devuelve null (por eso el tipo de retorno es Instructor?).
        return await _context.Instructors.FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public async Task<Instructor?> GetByIdWithCoursesAsync(Guid id, CancellationToken cancellationToken)
    {
        // ¡Este es el método clave que creamos para la regla de negocio de borrado!
        // .Include(i => i.Courses) es la instrucción MÁGICA de EF Core.
        // Le dice: "Cuando traigas el instructor, mira su propiedad 'Courses' y
        // trae también todos los cursos asociados a él en la misma consulta (haciendo un JOIN)".
        return await _context.Instructors
                             .Include(i => i.Courses)
                             .FirstOrDefaultAsync(i => i.Id == id, cancellationToken);
    }

    public void Update(Instructor instructor)
    {
        // EF Core tiene un sistema de "tracking" de cambios. Cuando cargamos un instructor
        // con GetByIdAsync, EF Core lo "vigila". Si modificamos una propiedad de ese objeto,
        // EF Core se da cuenta automáticamente. Por eso, el método Update puede estar vacío.
        // Sin embargo, es buena práctica explícitamente marcar el estado como modificado.
        _context.Entry(instructor).State = EntityState.Modified;
    }
}