using Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Courses.Commands.PublishCourse;

public class PublishCourseCommandHandler : IRequestHandler<PublishCourseCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public PublishCourseCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // Como es un IRequest, el método Handle devuelve solo Task.
    public async Task Handle(PublishCourseCommand request, CancellationToken cancellationToken)
    {
        // 1. Cargar la entidad raíz (el curso) junto con sus módulos.
        // Necesitamos los módulos para que la regla de negocio "no publicar curso vacío" pueda ser evaluada.
        var courseToPublish = await _unitOfWork.CourseRepository.GetByIdWithModulesAsync(request.CourseId, cancellationToken);

        // 2. Validar que el curso exista.
        if (courseToPublish is null)
        {
            throw new Exception($"Curso con ID '{request.CourseId}' no fue encontrado.");
        }

        // 3. ¡Delegar la lógica al Dominio!
        // El método `course.Publish()` contiene la regla que verifica si la lista de módulos no está vacía.
        // Si lo está, lanzará una InvalidOperationException.
        courseToPublish.Publish();

        // 4. Persistir el cambio de estado (IsPublished = true) en la base de datos.
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}