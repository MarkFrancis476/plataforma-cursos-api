using Application.Contracts.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class CourseRepository : ICourseRepository
{
    private readonly PlataformaCursosDbContext _context;

    public CourseRepository(PlataformaCursosDbContext context)
    {
        _context = context;
    }

    // --- IMPLEMENTACIÓN DE LOS MÉTODOS DE LA INTERFAZ ICourseRepository ---

    public async Task<Course?> GetByIdWithModulesAsync(Guid id, CancellationToken cancellationToken)
    {
        // La lógica es idéntica a la de GetByIdWithCoursesAsync, pero para Cursos y Módulos.
        // 1. Apuntamos a la tabla de Cursos (`_context.Courses`).
        // 2. Le decimos a EF Core que INCLUYA la colección de Módulos (`.Include(c => c.Modules)`).
        // 3. Buscamos el primer curso que coincida con el ID.
        return await _context.Courses
                             .Include(c => c.Modules)
                             .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}