namespace Domain.Entities;

public class Module
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public Guid CourseId { get; private set; }
    public Course Course { get; private set; }

    private readonly List<Lesson> _lessons = new();
    public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();

    private Module() {} // Para EF Core

    public Module(string title, Guid courseId)
    {
        Id = Guid.NewGuid();
        Title = title;
        CourseId = courseId;
    }
    
    // REGLA DE NEGOCIO: La regla de no poder añadir lecciones a un curso publicado
    // se maneja en el nivel superior (en el caso de uso), ya que el módulo
    // no sabe directamente si el curso está publicado. La lógica sería:
    // 1. Cargar el curso.
    // 2. Verificar si está publicado.
    // 3. Si no, cargar el módulo y añadirle la lección.
    public void AddLesson(Lesson lesson)
    {
        // Aquí podríamos tener reglas propias del módulo, si las hubiera.
        // Por ejemplo, "un módulo no puede tener más de 20 lecciones".
        _lessons.Add(lesson);
    }
}