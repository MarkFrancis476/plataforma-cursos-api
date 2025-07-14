using Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence;

public interface ICourseRepository
{
    // Este es el método que necesitamos: obtener un curso por su ID,
    // incluyendo su colección de módulos en la consulta.
    Task<Course?> GetByIdWithModulesAsync(Guid id, CancellationToken cancellationToken = default);
}