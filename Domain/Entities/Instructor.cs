namespace Domain.Entities;

public class Instructor
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    private readonly List<Course> _courses = new();
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

    private Instructor() {} // Para EF Core

    public Instructor(string name)
    {
        Id = Guid.NewGuid();
        Name = name;
    }
    
    public void UpdateName(string newName)
    {
        // Aquí podrían ir validaciones, como que el nombre no puede estar vacío.
        Name = newName;
    }

    // REGLA DE NEGOCIO CRÍTICA
    public bool CanBeDeleted()
    {
        // No se puede eliminar si tiene al menos un curso publicado.
        return !_courses.Any(c => c.IsPublished);
    }
}