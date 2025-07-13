namespace Domain.Entities;

public class Course
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public bool IsPublished { get; private set; }
    public Guid InstructorId { get; private set; }
    public Instructor Instructor { get; private set; }

    private readonly List<Module> _modules = new();
    public IReadOnlyCollection<Module> Modules => _modules.AsReadOnly();

    private Course() {} // Para EF Core

    public Course(string title, Guid instructorId)
    {
        Id = Guid.NewGuid();
        Title = title;
        InstructorId = instructorId;
        IsPublished = false;
    }

    // REGLA DE NEGOCIO CRÍTICA
    public void AddModule(Module module)
    {
        if (IsPublished)
        {
            throw new InvalidOperationException("No se pueden agregar módulos a un curso publicado.");
        }
        _modules.Add(module);
    }

    public void Publish()
    {
        if (!_modules.Any())
        {
            throw new InvalidOperationException("No se puede publicar un curso sin módulos.");
        }
        IsPublished = true;
    }
}