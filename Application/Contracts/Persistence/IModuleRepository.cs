using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence;

public interface IModuleRepository
{
    // El único método que necesitamos por ahora es este:
    // Obtener un módulo por su ID, incluyendo su lista de lecciones.
    Task<Module?> GetByIdWithLessonsAsync(Guid id, CancellationToken cancellationToken = default);
}