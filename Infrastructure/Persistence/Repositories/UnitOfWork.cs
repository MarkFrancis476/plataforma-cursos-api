using Application.Contracts.Persistence;
using Infrastructure.Persistence.DatabaseContext;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories;

// Esta clase implementa la interfaz IUnitOfWork que definimos en la capa de Application.
public class UnitOfWork : IUnitOfWork
{
    // --- DEPENDENCIAS Y PROPIEDADES ---

    // 1. Guardamos una referencia a nuestro DbContext. Es la conexión a la BD.
    private readonly PlataformaCursosDbContext _context;

    // 2. Propiedades para cada uno de los repositorios.
    // Estas propiedades implementan las que definimos en la interfaz IUnitOfWork.
    public IInstructorRepository InstructorRepository { get; }
    public ICourseRepository CourseRepository { get; }
    public IModuleRepository ModuleRepository { get; }


    // --- CONSTRUCTOR ---
    
    // El constructor recibe el DbContext a través de inyección de dependencias.
    public UnitOfWork(PlataformaCursosDbContext context)
    {
        _context = context;

        // ¡AQUÍ ESTÁ LA MAGIA!
        // Cuando se crea una instancia de UnitOfWork, también se crean las instancias
        // de cada uno de nuestros repositorios concretos.
        // Y lo más importante, ¡TODOS COMPARTEN EL MISMO DbContext!
        // Esto es crucial para que todos los cambios se hagan dentro de la misma transacción.
        InstructorRepository = new InstructorRepository(_context);
        CourseRepository = new CourseRepository(_context);
        ModuleRepository = new ModuleRepository(_context);
    }


    // --- MÉTODOS DE LA INTERFAZ ---

    // Implementación del método SaveChangesAsync de la interfaz.
    // Este es el método que nuestros Handlers llamarán para persistir todos los cambios.
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Simplemente delegamos la llamada al método SaveChangesAsync del DbContext.
        // Este método es el que realmente ejecuta los comandos INSERT, UPDATE o DELETE en la BD.
        return await _context.SaveChangesAsync(cancellationToken);
    }

    // Implementación del método Dispose.
    // Este método es parte de la interfaz IDisposable y se usa para liberar recursos,
    // en este caso, la conexión a la base de datos que mantiene el DbContext.
    public void Dispose()
    {
        _context.Dispose();
        // Le decimos al recolector de basura que no necesita llamar al finalizador de este objeto,
        // porque ya hemos limpiado los recursos nosotros mismos.
        GC.SuppressFinalize(this);
    }
}