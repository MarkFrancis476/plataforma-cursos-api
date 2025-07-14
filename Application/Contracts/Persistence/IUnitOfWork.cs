namespace Application.Contracts.Persistence;

public interface IUnitOfWork : IDisposable
{
    IInstructorRepository InstructorRepository { get; }
    ICourseRepository CourseRepository { get; }
    IModuleRepository ModuleRepository { get; } // <-- AÑADE ESTA LÍNEA

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}