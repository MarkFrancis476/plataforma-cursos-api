namespace Domain.Entities;

public class Lesson
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public Guid ModuleId { get; private set; }
    public Module Module { get; private set; }
    
    // Podría tener más propiedades como VideoUrl, Content, etc.

    private Lesson() {} // Para EF Core

    public Lesson(string title, Guid moduleId)
    {
        Id = Guid.NewGuid();
        Title = title;
        ModuleId = moduleId;
    }
}