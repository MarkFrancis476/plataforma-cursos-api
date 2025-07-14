using Application.Contracts.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Instructors.Commands.DeleteInstructor;

public class DeleteInstructorCommandHandler : IRequestHandler<DeleteInstructorCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInstructorCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
    {
        // 1. Cargar la entidad USANDO EL NUEVO MÉTODO.
        // Esto asegura que `instructor.Courses` venga cargado con datos.
        var instructorToDelete = await _unitOfWork.InstructorRepository.GetByIdWithCoursesAsync(request.Id, cancellationToken);

        // 2. Validar que exista.
        if (instructorToDelete is null)
        {
            throw new Exception($"Instructor con ID '{request.Id}' no fue encontrado.");
        }

        // 3. ¡AQUÍ ESTÁ LA MAGIA! APLICAR LA REGLA DE NEGOCIO DEL DOMINIO.
        // La capa de Application no sabe CÓMO se decide si se puede borrar.
        // Simplemente le pregunta a la entidad de dominio.
        if (!instructorToDelete.CanBeDeleted())
        {
            // Si el dominio dice "no", lanzamos un error claro.
            // Esta excepción la atraparemos en la capa de API para dar una respuesta HTTP 400 (Bad Request).
            throw new InvalidOperationException("No se puede eliminar un instructor que tiene cursos publicados.");
        }

        // 4. Si la validación del dominio pasa, procedemos a borrar.
        _unitOfWork.InstructorRepository.Delete(instructorToDelete);

        // 5. Guardar los cambios en la base de datos.
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}