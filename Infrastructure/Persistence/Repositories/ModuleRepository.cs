using Application.Contracts.Persistence;
using Domain.Entities;
using Infrastructure.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

public class ModuleRepository : IModuleRepository
{
    private readonly PlataformaCursosDbContext _context;

    public ModuleRepository(PlataformaCursosDbContext context)
    {
        _context = context;
    }

    // --- IMPLEMENTACIÓN DE LOS MÉTODOS DE LA INTERFAZ IModuleRepository ---

    public async Task<Module?> GetByIdWithLessonsAsync(Guid id, CancellationToken cancellationToken)
    {
        // Seguimos el mismo patrón.
        // 1. Apuntamos a la tabla de Módulos (`_context.Modules`).
        // 2. Le decimos que INCLUYA la colección de Lecciones (`.Include(m => m.Lessons)`).
        // 3. Buscamos el primer módulo que coincida con el ID.
        return await _context.Modules
                             .Include(m => m.Lessons)
                             .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
    }
}