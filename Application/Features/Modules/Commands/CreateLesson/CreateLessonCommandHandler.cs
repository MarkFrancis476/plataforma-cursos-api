using Application.Contracts.Persistence;
using Domain.Entities;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Modules.Commands.CreateLesson;

public class CreateLessonCommandHandler : IRequestHandler<CreateLessonCommand, Guid>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateLessonCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Handle(CreateLessonCommand request, CancellationToken cancellationToken)
    {
        // La regla de negocio de "no modificar un curso publicado" se aplica aquí de forma indirecta.
        // Si un atacante intenta añadir una lección a un módulo de un curso publicado, la API
        // debería primero verificar el estado del curso antes de enviar este comando.
        // Podríamos añadir esa lógica aquí para mayor seguridad.
        
        // 1. Cargar la entidad raíz (el módulo) usando el nuevo repositorio.
        var module = await _unitOfWork.ModuleRepository.GetByIdWithLessonsAsync(request.ModuleId, cancellationToken);

        // 2. Validar que el módulo exista.
        if (module is null)
        {
            throw new Exception($"Módulo con ID '{request.ModuleId}' no fue encontrado.");
        }

        // 3. Crear la nueva entidad de dominio (la lección).
        var newLesson = new Lesson(request.LessonTitle, module.Id);

        // 4. Delegar la acción a la entidad de dominio.
        module.AddLesson(newLesson);

        // 5. Persistir los cambios.
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        // 6. Devolver el ID de la nueva lección.
        return newLesson.Id;
    }
}