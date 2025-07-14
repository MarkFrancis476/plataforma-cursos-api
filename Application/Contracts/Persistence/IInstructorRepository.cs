using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Contracts.Persistence;

public interface IInstructorRepository
{
    // --- Métodos de Lectura (Queries) ---
    Task<Instructor?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Instructor>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Instructor?> GetByIdWithCoursesAsync(Guid id, CancellationToken cancellationToken = default);

    // --- Métodos de Escritura (Commands) ---
    Task AddAsync(Instructor instructor, CancellationToken cancellationToken = default);
    void Update(Instructor instructor);
    void Delete(Instructor instructor);
}